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
  public class PriorityController : ApiController
  {
    // GET api/values
    public IHttpActionResult Get()
    {
      var priorities = PriorityManager.Instance.GetAll();      
      return Json(priorities);
    }

    // GET api/values/5
    public IHttpActionResult Get(int id)
    {
      var Prioritys = Json(PriorityManager.Instance.GetById(id, "Priority"));
      var jsonString = JsonConvert.SerializeObject(Prioritys, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
      return Json(JsonConvert.DeserializeObject(jsonString));
    }
  }
}
