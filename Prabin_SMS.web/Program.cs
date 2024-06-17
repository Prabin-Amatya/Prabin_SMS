using IMS.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using Prabin_SMS.Infrastructure;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Infrastructure.Repository;
using Prabin_SMS.Infrastructure.Repository.CRUDServices;
using Prabin_SMS.web.Data;
using Prabin_SMS.web.Models;

var builder = WebApplication.CreateBuilder(args);
var ApplicationDb = builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.");

//var connectionString = builder.Configuration.GetConnectionString("Prabin_SMSwebContextConnection") ?? throw new InvalidOperationException("Connection string 'Prabin_SMSwebContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ApplicationDb, e => e.MigrationsAssembly("Prabin_SMS.web")));

builder.Services.AddDbContext<SMSDbContext>(options => options.UseSqlServer(ApplicationDb));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddTransient(typeof(ICRUDServices<>), typeof(CRUDServices<>));
builder.Services.AddTransient(typeof(IRawSqlRepository), typeof(RawSqlRepository));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(option =>
    {
        option.LoginPath = $"/Identity/Account/Login";
        option.LogoutPath = $"/Identity/Account/Logout";
        option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    });
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedingData.InitializeAsync(services);
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
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
