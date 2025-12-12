namespace SmartBudgetAI.Services
{
    /// Interface du service d'intelligence artificielle
    /// Utilise ML.NET pour prédire les catégories de transactions
    public interface IAiService
    {
        void TrainModel();
        string PredictCategory(string description);
    }
}