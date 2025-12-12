using Microsoft.ML.Data;
using System.Collections.Generic;

namespace SmartBudgetAI.Models
{
    /// <summary>
    /// Modèle d'entrée pour ML.NET : données à analyser
    /// Utilisé pour l'entraînement et la prédiction
    /// </summary>
    public class TransactionInput
    {
 
        /// Description de la transaction (feature principale pour la classification)
 
        [LoadColumn(0)]
        public string Description { get; set; } = string.Empty;

 
        /// Catégorie réelle (label) - utilisée uniquement lors de l'entraînement
 
        [LoadColumn(1)]
        public string Category { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modèle de sortie pour ML.NET : résultat de la prédiction
    /// </summary>
    public class CategoryPrediction
    {
 
        /// Catégorie prédite par le modèle ML.NET
 
        [ColumnName("PredictedLabel")]
        public string PredictedCategory { get; set; } = string.Empty;

 
        /// Score de confiance pour chaque catégorie possible
        /// Permet de connaître la probabilité de chaque catégorie
 
        [ColumnName("Score")]
        public float[] Scores { get; set; } = Array.Empty<float>();
    }

    /// <summary>
    /// Dataset d'entraînement fictif pour ML.NET
    /// En production, ces données viendraient d'une vraie base de données
    /// </summary>
    public static class TrainingData
    {
        public static List<TransactionInput> GetSampleData()
        {
            return new List<TransactionInput>
            {
                // Catégorie: Alimentation
                new() { Description = "Achat supermarché Carrefour", Category = "Alimentation" },
                new() { Description = "Restaurant McDonald's", Category = "Alimentation" },
                new() { Description = "Boulangerie pain croissants", Category = "Alimentation" },
                new() { Description = "Courses Leclerc fruits légumes", Category = "Alimentation" },
                new() { Description = "Livraison pizza Domino's", Category = "Alimentation" },
                new() { Description = "Café Starbucks", Category = "Alimentation" },
                
                // Catégorie: Transport
                new() { Description = "Essence station service Total", Category = "Transport" },
                new() { Description = "Ticket métro RATP", Category = "Transport" },
                new() { Description = "Uber course centre-ville", Category = "Transport" },
                new() { Description = "Péage autoroute A6", Category = "Transport" },
                new() { Description = "Parking aéroport", Category = "Transport" },
                new() { Description = "Réparation voiture garage", Category = "Transport" },
                
                // Catégorie: Logement
                new() { Description = "Loyer appartement", Category = "Logement" },
                new() { Description = "Facture électricité EDF", Category = "Logement" },
                new() { Description = "Eau Veolia", Category = "Logement" },
                new() { Description = "Internet fibre Orange", Category = "Logement" },
                new() { Description = "Assurance habitation", Category = "Logement" },
                new() { Description = "Meubles IKEA", Category = "Logement" },
                
                // Catégorie: Santé
                new() { Description = "Pharmacie médicaments", Category = "Santé" },
                new() { Description = "Consultation médecin généraliste", Category = "Santé" },
                new() { Description = "Dentiste soins", Category = "Santé" },
                new() { Description = "Mutuelle santé", Category = "Santé" },
                new() { Description = "Opticien lunettes", Category = "Santé" },
                
                // Catégorie: Loisirs
                new() { Description = "Cinéma places film", Category = "Loisirs" },
                new() { Description = "Abonnement Netflix", Category = "Loisirs" },
                new() { Description = "Salle de sport fitness", Category = "Loisirs" },
                new() { Description = "Livre Fnac", Category = "Loisirs" },
                new() { Description = "Concert billets", Category = "Loisirs" },
                new() { Description = "Jeu vidéo Steam", Category = "Loisirs" },
                
                // Catégorie: Shopping
                new() { Description = "Vêtements Zara", Category = "Shopping" },
                new() { Description = "Chaussures Nike", Category = "Shopping" },
                new() { Description = "Parfum Sephora", Category = "Shopping" },
                new() { Description = "Électronique Amazon", Category = "Shopping" },
                new() { Description = "Décoration maison", Category = "Shopping" },
                
                // Catégorie: Salaire (revenus)
                new() { Description = "Salaire mensuel", Category = "Salaire" },
                new() { Description = "Virement employeur", Category = "Salaire" },
                new() { Description = "Prime annuelle", Category = "Salaire" },
                new() { Description = "Freelance paiement client", Category = "Salaire" }
            };
        }
    }
}