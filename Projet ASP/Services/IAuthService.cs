using Microsoft.AspNetCore.Identity;
using SmartBudgetAI.Models;
using System.Threading.Tasks;

namespace SmartBudgetAI.Services
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