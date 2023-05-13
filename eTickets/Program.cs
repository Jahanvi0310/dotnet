using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using MySqlConnector;
using eTickets.Models;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    try
    {
        options.UseMySql(
            configuration.GetConnectionString("AppDbContext"),
            new MySqlServerVersion(new Version(8, 0, 26))
        );
        Console.WriteLine("connection done");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IActorsService,ActorsService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
    
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //Seed the database after and app start
    //if there is no database it will add it
 

app.Run();
