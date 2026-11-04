using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FITBOOM.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar el acceso al contexto HTTP y las sesiones
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// AGREGAR ESTO: Configurar el servicio de Autenticación por Cookies con su esquema por defecto
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Ruta a donde redirige si no está logueado
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Configuración del DbContext con resiliencia de conexión (EnableRetryOnFailure)
builder.Services.AddDbContext<FITBOOMContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexioSQL"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar el uso de sesiones en la aplicación
app.UseSession();

// AGREGAR ESTO: ¡Obligatoriamente ANTES de UseAuthorization!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();