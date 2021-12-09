// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Services.Data.Pattern
{
    /// Generic type parameters must be documented
    /// <summary>
    /// <c>Clase para administración del repositorio.</c>
    /// </summary>
    /// <typeparam name="TEntity">Type of the value to be returned</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Saves changes on given context against Database
        /// </summary>
        /// <returns><c>Int con el numero de filas afectadas.</c></returns>
        int SaveChanges();

        // void SaveChanges(SaveOptions option);

        /// <summary>
        /// Adds entity to the context
        /// </summary>
        /// <param name="entity"><c>Es la entidad que se insertara</c></param>
        /// <returns><c>Entidad que se agrega ala esquema de base de datos.</c></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Edits entity on the context
        /// </summary>
        /// <param name="entity"><c>Entity</c></param>
        void Edit(TEntity entity);

        /// <summary>
        /// Deletes entity from the context
        /// </summary>
        /// <param name="entity"><c>Entity</c></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes entity or entities from the context based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Deletes entity or entities from the context based on given specification
        /// </summary>
        /// <param name="specification"><c>specification</c></param>
        void Delete(ISpecification<TEntity> specification);

        /// <summary>
        /// Deletes entity and related entities from the context
        /// </summary>
        /// <param name="entity"><c>Entity</c></param>
        void DeleteRelatedEntities(TEntity entity);

        /// <summary>
        /// Gets all entities, Join with table
        /// </summary>
        /// <param name="tables">Table name for Join</param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> GetAll(params string[] tables);

        /// <summary>
        /// Gets all entities, Join with table and Where Predicate
        /// </summary>
        /// <param name="predicate"><c>Predicate for Where clausue</c></param>
        /// <param name="table"><c>Table name for Join</c></param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] table);

        /// <summary>
        /// Get all entities as paged
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns>IEnumerable of entities as paged</returns>
        IEnumerable<TEntity> GetAllPaged(int page, int pageSize);

        /// <summary>
        /// Finds entity based on given predicate
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Finds entity based on given specification
        /// </summary>
        /// <param name="specification"><c>specification</c></param>
        /// <returns>IEnumerable of entities</returns>
        IEnumerable<TEntity> Find(ISpecification<Func<TEntity, bool>> specification);

        /// <summary>
        /// Finds entities as paged based on given predicate
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <param name="predicate">where clause</param>
        /// <returns>IEnumerable of entities as paged</returns>
        IEnumerable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>Only one entity</returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, params string[] tables);

        /// <summary>
        /// Gets single entity
        /// </summary>
        /// <param name="specification"><c>specification</c></param>
        /// <returns>Only one entity</returns>
        TEntity Single(ISpecification<Func<TEntity, bool>> specification);

        /// <summary>
        /// Gets first entity
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <returns>First Entity</returns>
        TEntity First(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets first entity
        /// </summary>
        /// <param name="specification"><c>specification</c></param>
        /// <returns>First Entity</returns>
        TEntity First(ISpecification<Func<TEntity, bool>> specification);

        /// <summary>
        /// Gets count
        /// </summary>
        /// <returns>count of entities</returns>
        int Count();

        /// <summary>
        /// Gets count based on given criteria
        /// </summary>
        /// <param name="criteria">where clause</param>
        /// <returns>count of entities</returns>
        int Count(Expression<Func<TEntity, bool>> criteria);

        /// <summary>
        /// Gets count based on given specification
        /// </summary>
        /// <param name="specification"><c>specification</c></param>
        /// <returns>count of entities</returns>
        int Count(ISpecification<TEntity> specification);
    }
}