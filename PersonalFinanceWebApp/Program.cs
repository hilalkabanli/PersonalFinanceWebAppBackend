using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalFinanceWebApp.Data;
using PersonalFinanceWebApp.Helpers;
using PersonalFinanceWebApp.Models.Entities;
using PersonalFinanceWebApp.Service.Auth;
using PersonalFinanceWebApp.Services.User;
using PersonalFinanceWebApp.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<CurrencyController>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<AuthService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JwtAuthDotnetCore API", Version = "v1" });
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter your JWT token in this field",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.PrivateKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();


builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:5075")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://localhost:5075"));

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();