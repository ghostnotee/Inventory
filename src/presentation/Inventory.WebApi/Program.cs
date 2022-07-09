using FluentValidation.AspNetCore;
using Inventory.Application;
using Inventory.Data;
using Inventory.Data.Settings;

var builder = WebApplication.CreateBuilder(args);

DocumentMapping.MappingClasses();

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddCoreApplication();
builder.Services.AddInfrastructureData();

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

app.UseAuthorization();

app.MapControllers();

app.Run();