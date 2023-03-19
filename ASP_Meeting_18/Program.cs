using ASP_Meeting_18.AutoMapperProfiles;
using ASP_Meeting_18.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using System.Configuration;
using ASP_Meeting_18.Infrastructure.ModelBinderProviders;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

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
    name: "root",
 pattern: "/",
 defaults: new { controller = "Home", action = "Index", page = 1, category = (string)null }
 );
app.MapControllerRoute(
     name: "Default",
     pattern: "Admin/Page{page}",
     defaults: new { controller = "Admin", action = "Index", category = (string)null },
     constraints: new { page = @"\d+" });
app.MapControllerRoute(
     name: "Default",
     pattern: "Page{page}",
     defaults: new { controller = "Home", action = "Index", category = (string)null },
     constraints: new { page = @"\d+" });

app.MapControllerRoute(
    name: "cart",
    pattern: "cart/{action=Index}",
    defaults: new { controller = "Cart" }
 );

app.MapControllerRoute(
        name: "Category",
        pattern: "{controller=Admin}/{category:alpha}",
        defaults: new { controller = "Admin", action = "Index", page = 1 },
        constraints: new
        {
            controller = @"^(?!Home$|Cart$|Account$|Claims$|Roles$|User$).*"
        }
);
app.MapControllerRoute(
        name: "Category",
        pattern: "{controller=Home}/{category:alpha}",
        defaults: new { controller = "Home", action = "Index", page = 1 },
        constraints: new
        {
            controller = @"^(?!Admin$|Cart$|Account$|Claims$|Roles$|User$).*"
        }
);

app.MapControllerRoute(
    name: "CategoryPaging",
 pattern: "{category:alpha}/Page{page}",
 defaults: new { controller = "Home", action = "Index" },
 constraints: new { page = @"\d+" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
