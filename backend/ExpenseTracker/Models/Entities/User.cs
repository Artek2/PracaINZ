using Models.Entities.Abstraction;

namespace Models.Entities
{
  public class User : BaseEntity
  {
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public virtual List<IncomeExpense> IncomeExpense { get; set; } = null!;
  }
}
