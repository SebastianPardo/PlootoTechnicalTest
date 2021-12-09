using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Services.Data
{
  public partial class DataModel : DbContext
  {
    public DataModel()
        : base("name=DataModel")
    {
    }


    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
