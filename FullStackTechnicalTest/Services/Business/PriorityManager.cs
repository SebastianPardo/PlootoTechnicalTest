using Services.Data.Pattern;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Business
{
  public class PriorityManager
  {
    private static PriorityManager instance;

    public static PriorityManager Instance => instance ?? (instance = new PriorityManager());

    public List<Priority> GetAll(params string[] routes) => (List<Priority>)new Repository<Priority>().GetAll(routes);

    public Priority GetById(int id, params string[] routes) => new Repository<Priority>().Single(x => x.Id == id, routes);
   
  }
}