using ExpenseTracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Entities;
using Services.Authorization;

namespace Services
{
  public interface IExpenseService
  {
    public Task DeleteAsync(int id);
    public Task CreateAsync(IncomeExpenseRequest request);
    public Task<List<IncomeExpense>> GetAllAsync();
  }
  public class ExpenseService : IExpenseService
  {
    private readonly ExpenseTrackerDbContext _dbContext;
    private readonly IUserContextService _userContextService;
    private readonly IAuthorizationService _authorizationService;

    public ExpenseService(ExpenseTrackerDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
    {
      _dbContext = dbContext;
      _userContextService = userContextService;
      _authorizationService = authorizationService;
    }

    public async Task CreateAsync(IncomeExpenseRequest request)
    {
      if (_userContextService.GetUserId is not null)
      {
        var expense = new IncomeExpense
        {
          Description = request.Description,
          Amount = request.Amount,
          Title = request.Title,
          Type = "expense",
          Date = request.Date,
          Category = request.Category,
          UserId = _userContextService.GetUserId.Value,
        };
        await _dbContext.IncomeExpense.AddAsync(expense);
        await _dbContext.SaveChangesAsync();
      }
      else
      {
        throw new Exception("Something go wrong with api key");
      }
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
        .Where(i => i.Type == "expense" && i.UserId == _userContextService.GetUserId)
        .OrderByDescending(i => i.CreatedAt)
        .ToListAsync();
    }
  }
}
