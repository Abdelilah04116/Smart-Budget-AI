using Microsoft.AspNetCore.Identity;
using Projet_ASP.Models;
using System;
using System.Threading.Tasks;
using System.Linq;


namespace Projet_ASP.Services
{
    /// Service d'authentification utilisant ASP.NET Core Identity
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// Inscription d'un nouvel utilisateur
        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Connexion automatique après inscription
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        /// Connexion d'un utilisateur existant
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            ); 
        }   

        /// Déconnexion de l'utilisateur actuel
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}