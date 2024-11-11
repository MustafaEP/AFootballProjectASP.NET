using Access.Abstract.GenericDal;
using Access.Context;
using Access.EntityFramework.GenericRepository;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));


// Kimlik do�rulama servisini ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Giri� sayfas�n�n yolu
        options.AccessDeniedPath = "/Home/AccessDenied"; // Eri�im reddedildi�inde y�nlendirme yolu
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // �erez s�resi
    });
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

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


app.UseAuthentication(); // Kimlik do�rulamay� ekleyin
app.UseAuthorization();




app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
