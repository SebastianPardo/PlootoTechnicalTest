using Services.Models;
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
    public virtual DbSet<Priority> Priority { get; set; }

    public virtual DbSet<Task> Task { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
  }
}
