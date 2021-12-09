// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Services.Data.Pattern;

namespace Services.Data.Pattern
{
    /// Generic type parameters must be documented
    /// <summary>
    /// <c>Clase para administración de arboles.</c>
    /// </summary>
    /// <typeparam name="TEntity"><c>Object type TEntity</c></typeparam>+-
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// <c>Objeto de tipo TransversalModel en el que se define el contexto de datos a maipular.</c>
        /// </summary>
        private DataModel context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// <c>Método contructor de la clase, en este se inicializa el cotexto.</c>
        /// </summary>
        public Repository()
        {
            context = new DataModel();
            context.Configuration.ProxyCreationEnabled = false;

            // _context.ContextOptions.LazyLoadingEnabled = true; Default!
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// <c>Método contructor de la clase, en este se inicializa el cotexto.</c>
        /// </summary>
        /// <param name="lazyLoading"><c>Parametro lazyLoading, qu evita la precargarga de la inicializacion del contexto hasta su utilización</c></param>
        public Repository(bool lazyLoading = true)
        {
            context = new DataModel();

            // _context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.LazyLoadingEnabled = lazyLoading;
        }

        /// <summary>
        /// <c>Objeto DbSetTEntity, Objeto que se llena las entidades del contexto.</c>
        /// </summary>
        private DbSet<TEntity> EntitySet
        {
            get { return context.Set<TEntity>(); }
        }

        /// <summary>
        /// <c>Método para almacenar los cambios que hallan efectuado en el conexto.</c>
        /// </summary>
        /// <returns><c>Retorna un entero con el numero de entidades que se afectaron.</c></returns>
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        /// <summary>
        /// <c>Método para adicionar una entidad al contexto.</c>
        /// </summary>
        /// <param name="entity"><c>Entidas que se agregara el cntexto.</c></param>
        /// <returns><c>Retorna la entidad como se amaceno en el esquema de datos.</c></returns>
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return EntitySet.Add(entity);
        }

        /// <summary>
        /// <c>Método para actualizar una entidad al contexto.</c>
        /// </summary>
        /// <param name="entity"><c>Entidas que se agregara el contexto.</c></param>
        public void Edit(TEntity entity)
        {
            EntitySet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// <c>Método para eliminar una entidad al contexto.</c>
        /// </summary>
        /// <param name="entity"><c>Entidas que se eliminara en el contexto.</c></param>
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// <c>Método para eliminar una entidad al contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Predicado con el filtro o acciones que se desean implementar en la consulta.</c></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> records = Find(predicate);

            foreach (var record in records)
            {
                Delete(record);
            }
        }

        /// <summary>
        /// <c>Método para eliminar una entidad al contexto.</c>
        /// </summary>
        /// <param name="specification"><c>Predicado con el filtro o acciones que se desean implementar en la consulta.</c></param>
        public void Delete(ISpecification<TEntity> specification)
        {
        }

        /// <summary>
        /// <c>Método para eliminar una entidad al contexto.</c>
        /// </summary>
        /// <param name="entity"><c>Entidad que se desea eliminar y sus entidades relacionadas.</c></param>
        public void DeleteRelatedEntities(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            List<TEntity> releatedEntities =
                ((IEntityWithRelationships)entity).RelationshipManager.GetAllRelatedEnds().SelectMany(
                    e => e.CreateSourceQuery().OfType<TEntity>()).ToList();
            foreach (var releatedEntity in releatedEntities)
            {
                context.Entry(releatedEntity).State = EntityState.Deleted;
            }

            context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="tables"><c>Arreglo de string que define las propiedades e navegacion que deben ser consultadas en otras entidades por cada resgistro consultado.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> GetAll(params string[] tables)
        {
            if (tables.Count() == 0)
            {
                return EntitySet.ToList();
            }

            string path = tables[0];
            DbQuery<TEntity> inc = EntitySet.Include(path);
            for (int i = 1; i < tables.Length; i++)
            {
                path += "." + tables[i];
                inc = inc.Include(path);
            }

            return inc.ToList();
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Exprsion que describe los filtros a aplicar en la consulta.</c></param>
        /// <param name="tables"><c>Arreglo de string que define las propiedades e navegacion que deben ser consultadas en otras entidades por cada resgistro consultado.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] tables)
        {
            if (tables.Length > 0)
            {
                string path = tables[0];
                DbQuery<TEntity> inc = EntitySet.Include(path);
                for (int i = 1; i < tables.Length; i++)
                {
                    path += "." + tables[i];
                    inc = inc.Include(path);
                }

                return inc.Where(predicate).ToList();
            }

            return EntitySet.Where(predicate).ToList();
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="page"><c>Pagina en contexto que se esta manejando visualmente.</c></param>
        /// <param name="pageSize"><c>Numero de registros por pagina.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> GetAllPaged(int page, int pageSize)
        {
            return EntitySet.AsEnumerable().Skip(pageSize).Take(page);
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).AsEnumerable();
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="specification"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> Find(ISpecification<Func<TEntity, bool>> specification)
        {
            return null;
        }

        /// <summary>
        /// <c>Método para la consulta de la totalidad de registros para una entidad del contexto.</c>
        /// </summary>
        /// <param name="page"><c>Pagina en contexto que se esta manejando visualmente.</c></param>
        /// <param name="pageSize"><c>Numero de registros por pagina.</c></param>
        /// <param name="predicate"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public IEnumerable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).Skip(pageSize).Take(page).AsEnumerable();
        }

        /// <summary>
        /// <c>Método para la consulta un registro para una entidad del contexto.</c>
        /// </summary>
        /// <param name="specification"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Datos de la entidad.</c></returns>
        public TEntity Single(ISpecification<Func<TEntity, bool>> specification)
        {
            return null;
        }

        /// <summary>
        /// <c>Método para la consulta un registro para una entidad del contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <param name="tables"><c>Arreglo de string que define las propiedades e navegacion que deben ser consultadas en otras entidades por cada resgistro consultado.</c></param>
        /// <returns><c>Enumerable de datos de la entidad.</c></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate, params string[] tables)
        {
            if (tables.Length > 0)
            {
                string path = tables[0];
                DbQuery<TEntity> inc = EntitySet.Include(path);
                for (int i = 1; i < tables.Length; i++)
                {
                    path += "." + tables[i];
                    inc = inc.Include(path);
                }

                return inc.SingleOrDefault(predicate);
            }

            return EntitySet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// <c>Método para la consulta un registro para una entidad del contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <param name="tables"><c>Arbol de string que define las propiedades e navegacion que deben ser consultadas en otras entidades por cada resgistro consultado.</c></param>
        /// <returns><c>Datos de la entidad.</c></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate, MyTree<string> tables)
        {
            if (tables != null)
            {
                DbQuery<TEntity> inc = EntitySet;
                var nodes = new List<MyTree<string>>();
                nodes.Add(tables);
                MyTree<string> currentNode = null;
                do
                {
                    currentNode = nodes[0];
                    nodes.RemoveAt(0);
                    inc = inc.Include(currentNode.Path());
                    Dictionary<string, MyTree<string>> children = currentNode.GetChildren();
                    foreach (var child in children)
                    {
                        nodes.Add(child.Value);
                        inc = inc.Include(child.Value.Path());
                    }
                }
                while (nodes.Count > 0);
                return inc.SingleOrDefault(predicate);
            }

            return EntitySet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// <c>Método para la consulta un registro para una entidad del contexto.</c>
        /// </summary>
        /// <param name="predicate"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Datos de la entidad.</c></returns>
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.FirstOrDefault(predicate);
        }

        /// <summary>
        /// <c>Método para la consulta del primer registro para una entidad del contexto.</c>
        /// </summary>
        /// <param name="specification"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Datos de la entidad.</c></returns>
        public TEntity First(ISpecification<Func<TEntity, bool>> specification)
        {
            return null;
        }

        /// <summary>
        /// <c>Método para returnar el conteo de resgitro de una entidad.</c>
        /// </summary>
        /// <returns><c>Retorna un Int con el total del conteo realizado.</c></returns>
        public int Count()
        {
            return EntitySet.Count();
        }

        /// <summary>
        /// <c>Método para returnar el conteo de resgitro de una entidad.</c>
        /// </summary>
        /// <param name="criteria"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Retorna un Int con el total del conteo realizado.</c></returns>
        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return EntitySet.Where(criteria).Count();
        }

        /// <summary>
        /// <c>Método para returnar el conteo de resgitro de una entidad.</c>
        /// </summary>
        /// <param name="specification"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        /// <returns><c>Retorna un Int con el total del conteo realizado.</c></returns>
        public int Count(ISpecification<TEntity> specification)
        {
            return 0;
        }

        /// <summary>
        /// <c>Método que finaliza el conteto de datos.</c>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <c>Método que finaliza el conteto de datos.</c>
        /// </summary>
        /// <param name="disposing"><c>Expresion que dicta el termino de busqueda que se esta aplicando.</c></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (context == null)
            {
                return;
            }

            context.Dispose();
            context = null;
        }
    }
}