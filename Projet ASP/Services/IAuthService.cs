using Microsoft.AspNetCore.Identity;
using Projet_ASP.Models;
using System.Threading.Tasks;
using System.Linq;


namespace Projet_ASP.Services
{
    /// Interface du service d'authentification
    /// Définit les méthodes pour gérer l'inscription, connexion et déconnexion
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}