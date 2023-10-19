using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Data;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data.Data.Repository;
using ServiceApp.Models;
using ServiceApp.Data.Data.Initializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionSQL") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


//Add workUnit
builder.Services.AddScoped<IWorkUnit, WorkUnit>();

//Seeding data
builder.Services.AddScoped<IStarterDB, StarterDB>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

//Method to seed DB
SeedDB();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{Area=Client}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


//Method for seeding
void SeedDB()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbStarter = scope.ServiceProvider.GetRequiredService<IStarterDB>();
        dbStarter.Start();
    }
}