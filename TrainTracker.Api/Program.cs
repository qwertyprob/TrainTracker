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

// Fluent Validation API
builder.Services.AddFluentValidationAutoValidation();

//IncidentValidator
builder.Services.AddValidatorsFromAssemblyContaining<IncidentValidator>();


// DB Context
var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
            connectionString,
            new MariaDbServerVersion(new Version(12, 0, 2))
        )
        .LogTo(Console.WriteLine, LogLevel.Error); 
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
    app.UseExceptionHandler("/error"); // В API лучше общий endpoint для ошибок
    app.UseHsts();
}
//Automigration
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

app.UseHttpsRedirection();

// Роутинг
app.UseRouting();
app.UseAuthorization();

// Маршруты для API-контроллеров
app.MapControllers();

app.Run();