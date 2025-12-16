using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_ASP.Services;
using Projet_ASP.Models;
using Projet_ASP.Services;
using System.Security.Claims;

namespace Projet_ASP.Controllers
{
    /// Contrôleur principal pour la page d'accueil et le dashboard
    [Authorize] // Nécessite une authentification pour accéder
    public class HomeController : Controller
    {
        private readonly ITransactionService _transactionService;

        public HomeController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// Page d'accueil avec dashboard
        /// Route: /Home/Index ou /
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }

            var dashboardData = await _transactionService.GetDashboardDataAsync(userId);
            return View(dashboardData);
        }

        /// Page d'erreur
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}