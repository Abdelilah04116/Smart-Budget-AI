using Microsoft.AspNetCore.Identity;

namespace Projet_ASP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}