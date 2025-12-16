using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Projet_ASP.Models;
using Projet_ASP.Services;

namespace Projet_ASP.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var transactions = await _transactionService.GetAllByUserIdAsync(userId);
            return View(transactions);
        }

        public IActionResult Create()
        {
            var model = new Transaction
            {
                Date = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Erreur validation: {error.ErrorMessage}");
                }
                return View(transaction);
            }

            try
            {
                transaction.UserId = GetCurrentUserId();
                transaction.CreatedAt = DateTime.UtcNow;

                await _transactionService.CreateAsync(transaction);

                TempData["SuccessMessage"] = "Transaction créée avec succès !";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur création: {ex.Message}");
                ModelState.AddModelError("", $"Erreur lors de la création: {ex.Message}");
                return View(transaction);
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            ModelState.Remove("UserId");
            ModelState.Remove("User");

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

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("User not authenticated");
        }
    }
}