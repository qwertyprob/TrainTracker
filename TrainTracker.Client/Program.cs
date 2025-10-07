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

builder.Services.AddControllersWithViews();


//Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<IncidentValidator>();

var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
            connectionString,
            new MariaDbServerVersion(new Version(12, 0, 2))
        )
        .LogTo(Console.WriteLine, LogLevel.Error); 
});


//DI containers
//DAL
builder.Services.AddScoped<ITrainRepository, TrainRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();

//BLL
builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddSingleton<TrainJsonParser>();

//BackgroundServices
builder.Services.AddHostedService<TrainSimulationBackgroundService>();
builder.Services.AddHostedService<TrainDelayUpdateBackgroundService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Home/Error");
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



//Dashboard
app.MapControllerRoute(
    name: "dashboard",
    pattern: "{controller=Train}/{action=Trains}");


//Incidents
app.MapControllerRoute(
    name: "incidents",
    pattern: "{controller=Incident}/{action=Incidents}/{id}");



app.Run();