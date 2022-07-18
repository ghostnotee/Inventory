using FluentValidation.AspNetCore;
using Inventory.Application;
using Inventory.Application.Middlewares;
using Inventory.Data;
using Inventory.Data.Settings;
using Inventory.Identity;
using Inventory.Identity.Encryption;
using Inventory.Identity.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

DocumentMapping.MappingClasses();

// Add services to the container.
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});
builder.Services.AddInfrastructureIdentity();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddInfrastructureData();
builder.Services.AddCoreApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policyBuilder => policyBuilder.WithOrigins("http://localhost:5144"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder.WithOrigins("http://localhost:5144").AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionMiddleware();

app.MapControllers();

app.Run();