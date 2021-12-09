
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Models
{
  [Table("Task")]
  public class Task
  {
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the Code of Rate.
    /// </summary>
    [Required]
    [ForeignKey("Priority")]
    public int PriorityId { get; set; }

    [StringLength(250)]
    public string Description { get; set; }

    public bool Completed { get; set; }

    public virtual Priority Priority { get; set; }
  }
}