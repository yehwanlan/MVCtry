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

app.UseSession();        // <<<< �}�� Session�]�o�@��ܭ��n�^
app.UseAuthentication(); // <<<< �Y�� Identity �n�J�~�ݭn
app.UseAuthorization();

// �w�]����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// �Ȼs���ѡ]�d�ҡG�Ȥ�M�θ��ѡA�p�G�A�u���ݭn�^
app.MapControllerRoute(
    name: "customers",
    pattern: "customers1/{action=Index}/{CustomersID?}",
    defaults: new { controller = "Customers1" }
);

app.MapRazorPages();

app.Run();
