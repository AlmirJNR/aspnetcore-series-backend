using System.Reflection;
using Contracts.Categories.Interfaces;
using Contracts.Repositories.Interfaces;
using Contracts.SeriesContracts.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SeriesBackend.Repositories;
using SeriesBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
                      ?? throw new ArgumentException()));

// Repositories
builder.Services.AddTransient<ICategoryRepository, CategoriesRepository>();
builder.Services.AddTransient<ISeriesRepository, SeriesRepository>();

// Services
builder.Services.AddTransient<ICategoryService, CategoriesService>();
builder.Services.AddTransient<ISeriesService, SeriesService>();

// Controllers
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Series API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();