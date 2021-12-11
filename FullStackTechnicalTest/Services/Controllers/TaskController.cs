using Newtonsoft.Json;
using Services.Business;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Services.Controllers
{
  public class TaskController : ApiController
  {
    // GET api/values
    public IHttpActionResult Get()
    {
      var tasks = TaskManager.Instance.GetAll("Priority");
      var jsonString = JsonConvert.SerializeObject(tasks, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
      return Json(JsonConvert.DeserializeObject(jsonString));
    }

    // GET api/values/5
    public IHttpActionResult Get(int id)
    {
      var tasks = Json(TaskManager.Instance.GetById(id, "Priority"));
      var jsonString = JsonConvert.SerializeObject(tasks, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
      return Json(JsonConvert.DeserializeObject(jsonString));
    }

    // POST api/values
    public IHttpActionResult Post(Task entity)
    {
      return Json(TaskManager.Instance.Add(entity));
    }

    // PUT api/values/5
    public IHttpActionResult Put(Task entity)
    {
      return Json(TaskManager.Instance.Update(entity));
    }

    // DELETE api/values/5
    public IHttpActionResult Delete(int id)
    {
      return Json(TaskManager.Instance.Delete(id));
    }
  }
}
