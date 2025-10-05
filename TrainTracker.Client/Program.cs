using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TrainTracker.BLL.Interfaces;
using TrainTracker.BLL.Services;
using TrainTracker.DAL.Entities;
using TrainTracker.DAL.Interfaces;
using TrainTracker.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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


builder.Services.AddHostedService<TrainSimulationBackgroundService>();

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
    pattern: "{controller=Train}/{action=Index}");

//TrainInfo
app.MapControllerRoute(
    name: "traininfo",
    pattern: "{controller=Train}/{action=TrainInfo}/{id?}");

app.Run();