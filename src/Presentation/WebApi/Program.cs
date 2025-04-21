using Application;
using Application.GlobalExceptionHandler; 
using Domain.Entities;
using Domain.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Infrastructure;
using Persistence.Contexts;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


// -------------------- IDENTITY --------------------
builder.Services.AddDbContext<AppBaseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);

builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppBaseDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.SignIn.RequireConfirmedEmail = true;
    
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcçdefgðhiýjklmnoöpqrsþtuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@+/*|!,;:()&#?[] ";
});
// -------------------- IDENTITY --------------------


// -------------------- JWT Token --------------------
var tokenOptions = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>()!;
builder.Services.AddSingleton(tokenOptions);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }) 
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
            };
        }
    );
// -------------------- JWT Token --------------------


// -------------------- Rate Limiter --------------------
var _policyName = "sliding";
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddSlidingWindowLimiter(policyName: _policyName, slidingOptions =>
    {
        slidingOptions.PermitLimit = 10;
        slidingOptions.Window = TimeSpan.FromSeconds(15);
        slidingOptions.SegmentsPerWindow = 4;
        slidingOptions.QueueLimit = 2;
        slidingOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});


// -------------------- Layer Services --------------------
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
// -------------------- Layer Services--------------------


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

HttpContextHelper.HttpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseRateLimiter();

app.MapControllers().RequireRateLimiting(_policyName);

app.Run();
