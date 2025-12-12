using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_ASP.Services;
using SmartBudgetAI.Models;
using SmartBudgetAI.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartBudgetAI.Controllers
{
    /// Contrôleur CRUD pour les transactions
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAiService _aiService;

        public TransactionsController(
            ITransactionService transactionService,
            IAiService aiService)
        {
            _transactionService = transactionService;
            _aiService = aiService;
        }

        /// GET: Liste de toutes les transactions
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var transactions = await _transactionService.GetAllByUserIdAsync(userId);
            return View(transactions);
        }

        /// GET: Formulaire de création d'une transaction
        public IActionResult Create()
        {
            return View();
        }

        /// POST: Créer une nouvelle transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            transaction.UserId = GetCurrentUserId();
            await _transactionService.CreateAsync(transaction);

            TempData["SuccessMessage"] = "Transaction créée avec succès !";
            return RedirectToAction(nameof(Index));
        }

        /// GET: Formulaire d'édition d'une transaction
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();
            var transaction = await _transactionService.GetByIdAsync(id, userId);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        /// POST: Mettre à jour une transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            transaction.UserId = GetCurrentUserId();
            var result = await _transactionService.UpdateAsync(transaction);

            if (result == null)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Transaction mise à jour avec succès !";
            return RedirectToAction(nameof(Index));
        }

        /// GET: Page de confirmation de suppression
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            var transaction = await _transactionService.GetByIdAsync(id, userId);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        /// POST: Supprimer une transaction
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _transactionService.DeleteAsync(id, userId);

            if (!result)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Transaction supprimée avec succès !";
            return RedirectToAction(nameof(Index));
        }

        /// Méthode helper pour récupérer l'ID de l'utilisateur connecté
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("User not authenticated");
        }
    }
}