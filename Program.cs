using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticaApi;
using static PracticaApi.model;
var builder = WebApplication.CreateBuilder(args);
// Base de datos: un archivo SQLite llamado practica.db (se crea solo)
builder.Services.AddDbContext<AppDB>(o => o.UseSqlite("Data Source=practica.db"));
// Herramienta que cifra y verifica contrasenas
builder.Services.AddSingleton<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
// Autenticacion con JWT: aqui se le explica a la API como comprobar un token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =configJWT.Llave,
        ValidateLifetime = true          // un token vencido ya no sirve
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
var app = builder.Build();
// Al encender: crea la base de datos y una cuenta de prueba
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDB>();
    db.Database.EnsureCreated();
    if (!db.Usuarios.Any(u => u.correo == "prueba@practica.com"))
    {
        var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<Usuario>>();
        var prueba = new Usuario { correo = "prueba@practica.com", nombre = "Usuario de prueba" };
        prueba.claveHash = hasher.HashPassword(prueba, "123456");
        db.Usuarios.Add(prueba);
        db.SaveChanges();
    }
}
app.MapGet("/", () => "API de practica funcionando");
app.UseAuthentication();   // primero: quien eres (lee el token)
app.UseAuthorization();    // despues: que puedes hacer
app.MapControllers();
app.Run();