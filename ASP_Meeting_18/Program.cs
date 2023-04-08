using ASP_Meeting_18.AutoMapperProfiles;
using ASP_Meeting_18.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Configuration;
using ASP_Meeting_18.Infrastructure.ModelBinderProviders;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using Topshelf.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ShopProfile), typeof(UserProfile), typeof(EditUserProfile));
// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllersWithViews(options =>
    options.ModelBinderProviders.Insert(0, new CartModelBinderProvider()));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ShopDbContext>();
string connStr = builder.Configuration.GetConnectionString("shopDb");
builder.Services.AddDbContext<ShopDbContext>(options =>
options.UseSqlServer(connStr));
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddControllersWithViews()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

// Set the culture to use the period as the decimal separator
var cultureInfo = new CultureInfo("en-US");
    cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("FrameworkPolicy", policy =>
    {
        policy.RequireClaim("PrefferedFramework", new[] { "ASP.NET Core" });
        policy.RequireRole("admin", "manager");
    });
    option.AddPolicy("AdminPolicy", policy =>
     {
         policy.RequireRole("admin");
     });
});
builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection googleSection = configuration.GetSection("Authentication:Google");
    options.ClientId = googleSection.GetValue<string>("ClientId");
    options.ClientSecret = googleSection["ClientSecret"];
}).AddFacebook(fbOptions => {
    IConfigurationSection facebookSection = configuration.GetSection("Authentication:Facebook");
    fbOptions.AppId = facebookSection.GetSection("AppId").Value;
    fbOptions.AppSecret = facebookSection.GetSection("AppSecret").Value;
}
);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await SeedData.Initialize(serviceProvider,app.Environment);
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ShopDbContext>();
        await context.Database.EnsureCreatedAsync();
        await EnsureRolesAsync(services, "User");
        await EnsureRolesAsync(services, "Admin");
        await EnsureDefaultUserAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

async Task EnsureRolesAsync(IServiceProvider services, string roleName)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}

async Task EnsureDefaultUserAsync(IServiceProvider services)
{
    var userManager = services.GetRequiredService<UserManager<User>>();
    var user = await userManager.FindByNameAsync("admin@example.com");
    if (user == null)
    {
        user = new User
        {
            UserName = "AdminDaniil",
            Email = "admin@example.com",
            YearOfBirth = 2003
        };
        await userManager.CreateAsync(user, "IAmAdminNumber1!");
        await userManager.AddToRoleAsync(user, "Admin");
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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
