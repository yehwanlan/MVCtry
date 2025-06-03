using CUSTOMERWEBSITE.Data;
using CUSTOMERWEBSITE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var northwindConn = builder.Configuration.GetConnectionString("Northwind")
    ?? throw new InvalidOperationException("Connection string 'Northwind' not found.");
builder.Services.AddDbContext<NorthwindContext>(options =>
    options.UseSqlServer(northwindConn));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".CustomersWebsite.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();        // <<<< 開啟 Session（這一行很重要）
app.UseAuthentication(); // <<<< 若有 Identity 登入才需要
app.UseAuthorization();

// 預設路由
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 客製路由（範例：客戶專用路由，如果你真的需要）
app.MapControllerRoute(
    name: "customers",
    pattern: "customers1/{action=Index}/{CustomersID?}",
    defaults: new { controller = "Customers1" }
);

app.MapRazorPages();

app.Run();
