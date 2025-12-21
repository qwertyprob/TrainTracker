using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TrainTracker.BLL.Interfaces;
using TrainTracker.BLL.Services;
using TrainTracker.BLL.Services.BackgroundServices;
using TrainTracker.DAL.Entities;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DAL.Repositories;
using TrainTracker.Validators;

var builder = WebApplication.CreateBuilder(args);

// Подключаем только API-контроллеры
builder.Services.AddControllers();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Fluent Validation API
builder.Services.AddFluentValidationAutoValidation();

//IncidentValidator
builder.Services.AddValidatorsFromAssemblyContaining<IncidentValidator>();

//front
var allowedOrigins = new[]
{
    "http://localhost:3000",
    "http://127.0.0.1:3000",
    "http://localhost:5174",
    "http://127.0.0.1:5174",
    "http://127.0.0.1:5173",
    "http://localhost:5173"
};

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(optionsCORS =>
    {
        optionsCORS
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


// DB Context

var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
#if DEBUG
    connectionString = builder.Configuration.GetConnectionString("LocalString");
#endif

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MariaDbServerVersion(new Version(12, 0, 2))
    );
});


// DI контейнеры
// DAL
builder.Services.AddScoped<ITrainRepository, TrainRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

// BLL
builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddSingleton<TrainJsonParser>();

// BackgroundServices
builder.Services.AddHostedService<TrainSimulationBackgroundService>();
builder.Services.AddHostedService<TrainDelayUpdateBackgroundService>();


var app = builder.Build();

// HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "TrainApi";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty; // Swagger на корне сайта
    });

    app.UseHsts();
}

//Automigration

    
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }



app.UseHttpsRedirection();

// Роутинг
app.UseRouting();
app.UseCors(); 
app.UseAuthorization();

// Маршруты для API-контроллеров
app.MapControllers();

app.Run();