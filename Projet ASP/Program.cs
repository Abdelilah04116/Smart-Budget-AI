using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projet_ASP.Data;
using Projet_ASP.Models;
using Projet_ASP.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données SQL Server Express
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration d'ASP.NET Core Identity pour l'authentification
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configuration des règles de mot de passe
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Configuration de l'utilisateur
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configuration des cookies d'authentification
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

// Enregistrement des services personnalisés (Dependency Injection)
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IAiService, AiService>(); // Singleton car le modèle ML est lourd

// Ajout des contrôleurs et des vues
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware d'authentification et d'autorisation
app.UseAuthentication();
app.UseAuthorization();

// Configuration des routes MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialisation de la base de données et du modèle ML au démarrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Création automatique de la base de données si elle n'existe pas
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        // Entraînement du modèle ML si nécessaire
        var aiService = services.GetRequiredService<IAiService>();
        aiService.TrainModel();

        Console.WriteLine("✅ Base de données et modèle ML initialisés avec succès.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erreur lors de l'initialisation : {ex.Message}");
    }
}

app.Run();