using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services
{
  //uzyskanie aktualnie zalogowanego użytkownika i jego identyfikatora.
  
  public interface IUserContextService
  {
    ClaimsPrincipal User { get; }
    int? GetUserId { get; }
  }
  public class UserContextService : IUserContextService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;

    public int? GetUserId =>
        User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
  }
}
