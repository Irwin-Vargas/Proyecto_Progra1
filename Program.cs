using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ✅ Habilitar Razor Pages y configuración de validación
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    });

// ✅ Agregar caché distribuida (para que las sesiones funcionen)
builder.Services.AddDistributedMemoryCache(); // 🔥 NECESARIO para sesiones

// ✅ Agregar configuración de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Habilitar acceso a `HttpContext`
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();         // ✅ Habilitar sesión (después de UseRouting)
app.UseAuthentication();  // ✅ Habilitar autenticación
app.UseAuthorization();   // ✅ Habilitar autorización

app.MapControllerRoute(
    name: "carrito",
    pattern: "{controller=Carrito}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "orden",
    pattern: "{controller=Orden}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    
app.MapControllerRoute(
    name: "registro",
    pattern: "{controller=User}/{action=Registro}/{id?}");


app.Run();
