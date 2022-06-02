using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PanGainsWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PanGainsWebAppContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("PanGainsMySql"), new MySqlServerVersion(new Version(8, 0, 22))));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
