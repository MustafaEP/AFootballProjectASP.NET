using Access.Context;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Business.Concrete.Image;
using Business.Abstract.Image;
using Core.Access.EntityFramework.GenericRepository;
using Core.Access.Abstract.GenericDal;
using Business.Concrete.NewVersion;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IImageSearchService, ImageSearchManager>();

// Kimlik doðrulama servisini ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; 
        options.AccessDeniedPath = "/Home/AccessDenied"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
    });
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));

    options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");

app.UseRouting();


app.UseAuthentication(); // Kimlik doðrulamayý ekleyin
app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
