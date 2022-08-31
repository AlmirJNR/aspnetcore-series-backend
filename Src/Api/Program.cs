using System.Reflection;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SeriesBackend.Repositories;
using SeriesBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql("Server=postgresqldb;Port=5432;Database=postgres;User Id=postgres;Password=password;"));

builder.Services.AddSingleton<CategoriesRepository>();
builder.Services.AddSingleton<CategoriesService>();
builder.Services.AddSingleton<SeriesRepository>();
builder.Services.AddSingleton<SeriesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Series API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    config.IncludeXmlComments(xmlPath);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("*", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("*");

app.UseAuthorization();

app.MapControllers();

app.Run();