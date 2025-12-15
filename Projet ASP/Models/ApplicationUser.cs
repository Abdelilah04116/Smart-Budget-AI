using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Projet_ASP.Models
{
     
    /// Modèle utilisateur étendu basé sur IdentityUser
    /// Hérite de IdentityUser pour bénéficier de toutes les fonctionnalités d'Identity
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
         
        /// Nom complet de l'utilisateur
 
        public string? FullName { get; set; }

         
        /// Date de création du compte
 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

         
        /// Navigation property : Liste des transactions de l'utilisateur
        /// Permet à EF Core de gérer la relation One-to-Many
 
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}