namespace ExpenseTracker
{
  public class IncomeExpenseRequest
  {
    public string Title { get; set; }
    public int Amount { get; set; }
    public DateOnly Date { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
  }
}
