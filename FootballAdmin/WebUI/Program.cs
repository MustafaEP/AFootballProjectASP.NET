using Access.Abstract.GenericDal;
using Access.Context;
using Access.EntityFramework.GenericRepository;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));

// Diðer servis yapýlandýrmalarý
builder.Services.AddControllersWithViews();

// Kimlik doðrulama servisini ekleyin
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Home/Login";
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
