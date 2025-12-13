using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBudgetAI.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SmartBudgetAI.Data
{
    /// DbContext principal de l'application
    /// Hérite de IdentityDbContext pour intégrer ASP.NET Core Identity
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// Table des transactions
        public DbSet<Transaction> Transactions { get; set; }

        /// Configuration avancée des entités avec Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuration de la relation User -> Transactions (One-to-Many)
            builder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade

            // Index pour améliorer les performances des requêtes
            builder.Entity<Transaction>()
                .HasIndex(t => t.UserId);

            builder.Entity<Transaction>()
                .HasIndex(t => t.Date);

            builder.Entity<Transaction>()
                .HasIndex(t => t.Category);

            // Configuration précise du type decimal pour SQL Server
            builder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            // Données de seed (optionnel - pour tests)
            // On peut ajouter des données initiales ici si nécessaire
        }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}