using Microsoft.EntityFrameworkCore;
using Projet_ASP.Data;
using Projet_ASP.Models;

namespace Projet_ASP.Services
{
    /// Service de gestion des transactions avec logique métier
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// Récupère toutes les transactions d'un utilisateur
        public async Task<List<Transaction>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        /// <summary>
        /// Récupère une transaction par ID (avec vérification de propriété)
        /// </summary>
        public async Task<Transaction?> GetByIdAsync(int id, string userId)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        /// Crée une nouvelle transaction
        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            transaction.CreatedAt = DateTime.UtcNow;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        /// Met à jour une transaction existante
        public async Task<Transaction?> UpdateAsync(Transaction transaction)
        {
            var existing = await GetByIdAsync(transaction.Id, transaction.UserId);
            if (existing == null)
                return null;

            existing.Description = transaction.Description;
            existing.Amount = transaction.Amount;
            existing.Category = transaction.Category;
            existing.Date = transaction.Date;
            existing.IsAiCategorized = transaction.IsAiCategorized;

            _context.Transactions.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        /// Supprime une transaction
        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var transaction = await GetByIdAsync(id, userId);
            if (transaction == null)
                return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        /// Génère les données pour le dashboard
        public async Task<DashboardViewModel> GetDashboardDataAsync(string userId)
        {
            var transactions = await GetAllByUserIdAsync(userId);

            var viewModel = new DashboardViewModel
            {
                TransactionCount = transactions.Count,
                TotalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount),
                TotalExpenses = Math.Abs(transactions.Where(t => t.Amount < 0).Sum(t => t.Amount)),
                RecentTransactions = transactions.Take(10).ToList()
            };

            viewModel.Balance = viewModel.TotalIncome - viewModel.TotalExpenses;

            // Regroupement des dépenses par catégorie
            viewModel.ExpensesByCategory = transactions
                .Where(t => t.Amount < 0)
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => Math.Abs(g.Sum(t => t.Amount)));

            // Regroupement des revenus par catégorie
            viewModel.IncomeByCategory = transactions
                .Where(t => t.Amount > 0)
                .GroupBy(t => t.Category)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

            return viewModel;
        }
    }
}