using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Muma.Api.JWT;
using Muma.Api.Swagger;
using Muma.Application;
using Muma.Infrastructure;
using Muma.Infrastructure.Data.Seeder;
using System.Text;


var corsOriginsPolicyName = "CORSAllowAnyOrigin";

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsOriginsPolicyName, 
                builder =>
                {
                    builder.AllowAnyOrigin() 
                        .AllowAnyHeader() 
                        .AllowAnyMethod(); 
                });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services
    .AddMumaApplication()
    .AddMumaInfrastructure(builder.Configuration);

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x => 
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true

        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IJWTHelper, JWTHelper>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(corsOriginsPolicyName);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await DataSeeder.Seed(app.Services);

app.Run();
