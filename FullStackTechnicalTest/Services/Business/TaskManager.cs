using Services.Data.Pattern;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business
{
  public class TaskManager
  {
    private static TaskManager instance;

    public static TaskManager Instance => instance ?? (instance = new TaskManager());

    public List<Task> GetAll(params string[] routes) => (List<Task>)new Repository<Task>().GetAll(routes);

    public Task GetById(int id, params string[] routes) => new Repository<Task>().Single(x => x.Id == id, routes);

    public Task Add(Task entity)
    {
      using (var repo = new Repository<Task>())
      {
        Task pt = repo.Single(c => c.Id == entity.Id);
        if (pt == null)
        {
          pt = repo.Add(entity);
          repo.SaveChanges();
          return pt;
        }

        return null;
      }
    }

    public bool Update(Task entity)
    {
      using (var repo = new Repository<Task>())
      {
        repo.Edit(entity);
        return repo.SaveChanges() > 0;
      }
    }

    public bool Delete(Task Task)
    {
      using (var repo = new Repository<Task>())
      {
        repo.Delete(Task);
        return repo.SaveChanges() > 0;
      }
    }
  }
}