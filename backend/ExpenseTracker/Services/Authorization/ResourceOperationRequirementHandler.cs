using Microsoft.AspNetCore.Authorization;
using Models.Entities;
using System.Security.Claims;

namespace Services.Authorization
{
  public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, IncomeExpense>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
        IncomeExpense incomeExpense)
    {
      if (requirement.ResourceOperation == ResourceOperation.Read ||
          requirement.ResourceOperation == ResourceOperation.Create)
      {
        context.Succeed(requirement);
      }

      var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
      if (incomeExpense.UserId == int.Parse(userId))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}
