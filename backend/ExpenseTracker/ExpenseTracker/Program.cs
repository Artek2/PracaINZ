using ExpenseTracker;
using ExpenseTracker.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Models.Entities;
using Services;
using Services.Authorization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// configre service
var authenticationSettings = new AuthenticationSettings();

//mssql
//builder.Services.AddDbContext<ExpenseTrackerDbContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("VIATMS2Context")));

//mysql
var detected = ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbConnection"));
builder.Services.AddDbContext<ExpenseTrackerDbContext>
    (options =>
    {
      options.UseMySql(builder.Configuration.GetConnectionString("DbConnection"), detected);
    });

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
  option.DefaultAuthenticateScheme = "Bearer";
  option.DefaultScheme = "Bearer";
  option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
  cfg.RequireHttpsMetadata = false;
  cfg.SaveToken = true;
  cfg.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = authenticationSettings.JwtIssuer,
    ValidAudience = authenticationSettings.JwtIssuer,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
  };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
  opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
  opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "bearer"
  });

  opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors(builder =>
builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
