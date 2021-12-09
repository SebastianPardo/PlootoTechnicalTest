using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
  [Table("Priority")]
  public class Priority
  {
    [Key]
    public int Id { get; set; }

    public int Level { get; set; }

    [StringLength(50)]
    public string Description { get; set; }

    public virtual ICollection<Task> Task { get; set; }

  }
}