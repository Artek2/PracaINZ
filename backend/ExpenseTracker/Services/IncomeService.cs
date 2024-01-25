using ExpenseTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Entities;
using Services.Authorization;

namespace Services
{
  public interface IIncomeService
  {
    public Task DeleteAsync(int id);
    public Task CreateAsync(IncomeExpenseRequest request);
    public Task<List<IncomeExpense>> GetAllAsync();

  }
  public class IncomeService : IIncomeService
  {
    private readonly ExpenseTrackerDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public IncomeService(ExpenseTrackerDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
    {
      _dbContext = dbContext;
      _userContextService = userContextService;
      _authorizationService = authorizationService;
    }

    public async Task CreateAsync(IncomeExpenseRequest request)
    {
      var income = new IncomeExpense
      {
        Amount = request.Amount,
        Description = request.Description,
        Title = request.Title,
        Type = "income",
        Date = request.Date,
        Category = request.Category,
        UserId = _userContextService.GetUserId.Value,
      };
      await _dbContext.IncomeExpense.AddAsync(income);
      await _dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
      var income = await _dbContext.IncomeExpense.FirstOrDefaultAsync(f => f.Id == id);

      var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, income,
    new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

      if (!authorizationResult.Succeeded)
      {
        throw new Exception("Nie masz dostępu");
      }

      _dbContext.IncomeExpense.Remove(income);
      await _dbContext.SaveChangesAsync();
    }

    public Task<List<IncomeExpense>> GetAllAsync()
    {
      return _dbContext
        .IncomeExpense
        .Where(i => i.Type == "income" && i.UserId == _userContextService.GetUserId)
        .OrderByDescending(i => i.CreatedAt)
        .ToListAsync();
    }
  }
}
