using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBudgetAI.Models
{
    /// Modèle représentant une transaction financière
    public class Transaction
    {
 
        /// Identifiant unique de la transaction
 
        [Key]
        public int Id { get; set; }

 
        /// Description de la transaction (ex: "Achat supermarché Carrefour")
 
        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        public string Description { get; set; } = string.Empty;

 
        /// Montant de la transaction (positif pour revenu, négatif pour dépense)
 
        [Required(ErrorMessage = "Le montant est obligatoire")]
        [Range(-1000000, 1000000, ErrorMessage = "Le montant doit être entre -1 000 000 et 1 000 000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

 
        /// Catégorie de la transaction (ex: "Alimentation", "Transport", "Salaire")
        /// Peut être définie manuellement ou automatiquement par l'IA
 
        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

 
        /// Date de la transaction
 
        [Required(ErrorMessage = "La date est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

 
        /// Indique si la catégorie a été prédite par l'IA
 
        public bool IsAiCategorized { get; set; } = false;

 
        /// Date de création dans la base de données
 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

 
        /// Foreign Key : ID de l'utilisateur propriétaire
 
        [Required]
        public string UserId { get; set; } = string.Empty;

 
        /// Navigation property : Utilisateur propriétaire de la transaction
 
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}