using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_ASP.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le montant est obligatoire")]
        [Range(-1000000, 1000000, ErrorMessage = "Le montant doit être entre -1 000 000 et 1 000 000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "La catégorie est obligatoire")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        public bool IsAiCategorized { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}