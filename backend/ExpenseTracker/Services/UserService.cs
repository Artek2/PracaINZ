using ExpenseTracker;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Entities;
using Services.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Services
{
  public interface IUserService
  {
    public Task RegisterUserAsync(RegisterUserDto register);
    public Task<(string, string)> GenerateJwt(LoginRequest dto);
  }
  public class UserService : IUserService
  {
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ExpenseTrackerDbContext _dbContext;
    private readonly AuthenticationSettings _authenticationSettings;

    public UserService(ExpenseTrackerDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
      _dbContext = dbContext;
      _passwordHasher = passwordHasher;
      _authenticationSettings = authenticationSettings;
    }

    public async Task RegisterUserAsync(RegisterUserDto register)
    {
      var user = _dbContext.Users
          .FirstOrDefault(u => u.Email == register.Email);

      if (user is not null)
      {
        throw new Exception("Email exist in database");
      }
      var newUser = new User()
      {
        Email = register.Email,
        Name = register.Name,
      };

      if (register.Password != register.ConfirmPassword)
      {
        throw new Exception("Password not match");
      }
      var hashedPassword = _passwordHasher.HashPassword(newUser, register.Password);

      newUser.PasswordHash = hashedPassword;
      _dbContext.Users.Add(newUser);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<(string, string)> GenerateJwt(LoginRequest dto)
    {
      var user = _dbContext.Users
          .FirstOrDefault(u => u.Email == dto.Email);

      if (user is null)
      {
        throw new Exception("Invalid email or password");
      }

      var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
      if (result == PasswordVerificationResult.Failed)
      {
        throw new Exception("Invalid email or password");
      }

      var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
      var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

      var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
          _authenticationSettings.JwtIssuer,
          claims,
          expires: expires,
          signingCredentials: cred);

      var tokenHandler = new JwtSecurityTokenHandler();
      return (tokenHandler.WriteToken(token), user.Name);
    }
  }
}
