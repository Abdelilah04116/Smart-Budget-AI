using Projet_ASP.Models;

namespace Projet_ASP.Services
{
    /// Interface du service de gestion des transactions
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllByUserIdAsync(string userId);
        Task<Transaction?> GetByIdAsync(int id, string userId);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(int id, string userId);
        Task<DashboardViewModel> GetDashboardDataAsync(string userId);
    }
}