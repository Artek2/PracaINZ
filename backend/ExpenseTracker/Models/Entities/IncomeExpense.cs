using Models.Entities.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
  public class IncomeExpense : BaseEntity
  {
    [StringLength(50)]
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }

    [StringLength(50)]
    public string Type { get; set; } = string.Empty;
    public DateOnly Date { get; set; }

    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [StringLength(255)]
    public string Description { get; set; } = string.Empty;
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
  }
}
