using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SystemName.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<DatabaseContext>(options => //allow atabsecontect tointeract with databse
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DatabaseContext")));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddSession(options => //makes session option
{
    
    options.IdleTimeout = TimeSpan.FromMinutes(30); // adjust session timeout
});


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

app.UseAuthentication(); //used to attempt roles
app.UseAuthorization();

app.UseSession(); //starts seeson

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

//var options = new RewriteOptions()  //redirects https://localhost:****/ to https://localhost:****/Home. since they 
//    .AddRedirect("^$", "Home");

//app.UseRewriter(options);

app.Run();
