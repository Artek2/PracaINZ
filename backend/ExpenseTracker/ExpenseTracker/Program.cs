// Namespace imports from the .NET and ASP.NET Core frameworks and custom namespaces
using ExpenseTracker; // Custom configuration or utility namespace
using ExpenseTracker.Middleware; // Custom middleware for error handling
using Microsoft.AspNetCore.Authorization; // ASP.NET Core's Authorization framework
using Microsoft.AspNetCore.Identity; // ASP.NET Core Identity for user management and authentication
using Microsoft.EntityFrameworkCore; // Entity Framework Core for database context and ORM functionality
using Microsoft.IdentityModel.Tokens; // Part of the System.IdentityModel.Tokens.Jwt for handling JWT tokens
using Microsoft.OpenApi.Models; // Used for configuring Swagger/OpenAPI documentation
using Models; // Custom namespace likely containing entity models
using Models.Entities; // Specific namespace for entity models
using Services; // Custom namespace for business logic services
using Services.Authorization; // Custom namespace for authorization services
using System.Text; // System.Text namespace for encoding utilities

var builder = WebApplication.CreateBuilder(args);
// Configure service
var authenticationSettings = new AuthenticationSettings();

// Database context configuration for MySQL with connection string retrieval and server version detection
var detected = ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DbConnection"));
builder.Services.AddDbContext<ExpenseTrackerDbContext>
    (options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("DbConnection"), detected);
    });

// Binding settings from configuration
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

// Singleton service registration for the authentication settings
builder.Services.AddSingleton(authenticationSettings);

// Authentication configuration using JWT Bearer scheme
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

// Standard service registration for controllers and Swagger in the .NET Core application
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    // Security definition for Swagger to understand JWT authorization
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    // Security requirements for Swagger to use the defined security
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
// Scoped services for various application functionalities including user management and middleware
builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Building the application
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>(); // Use custom error handling middleware

// Configure the HTTP request pipeline, including Swagger and CORS configurations
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

app.UseHttpsRedirection(); // Use HTTPS redirection middleware

app.UseAuthorization(); // Use authorization middleware

app.MapControllers(); // Map attribute-routed controllers

app.Run(); // Run the application
