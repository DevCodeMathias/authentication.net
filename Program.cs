using Amazon.SQS;
using API_AUTENTICATION.application.Service;
using API_AUTENTICATION.config;
using API_AUTENTICATION.domain.Interfaces.Repository;
using API_AUTENTICATION.domain.Interfaces.Service;
using API_AUTENTICATION.infrastructure.middleware;
using authentication_API.infrastructure.data;
using authentication_API.infrastructure.repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Amazon.SimpleNotificationService;

var builder = WebApplication.CreateBuilder(args);

// Bind configurações do JWT a classe JwtConfig
var jwtConfigSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtConfig>(jwtConfigSection);

// Registra JwtConfig para injeção via IOptions<JwtConfig>
var jwtConfig = jwtConfigSection.Get<JwtConfig>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Para usar JwtConfig em outros serviços via injeção de dependência, registre aqui
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<ITokenService>(new TokenService(jwtConfig.SecretKey, jwtConfig.Issuer, jwtConfig.Audience));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.AddAWSService<IAmazonSimpleNotificationService>();

builder.Services.AddScoped<IUserQueueSender, VerifyUserQueueSender>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IPublishService, PublishService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
