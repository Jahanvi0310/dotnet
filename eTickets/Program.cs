using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using eTickets.Models;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using eTickets.Data.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using eTickets.Data.Static;

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
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Add role "USER" if it doesn't exist
    var roleExists = await roleManager.RoleExistsAsync(UserRoles.User);
    if (!roleExists)
    {
        var role = new IdentityRole(UserRoles.User);
        await roleManager.CreateAsync(role);
    }

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
app.UseAuthentication(); // Place it here
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
