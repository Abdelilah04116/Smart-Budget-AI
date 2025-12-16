using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.ML.Data;
using Projet_ASP.Models;
using System;
using System.IO;
using System.Linq;


namespace Projet_ASP.Services
{

    /// Service d'IA utilisant ML.NET pour la classification de texte
    /// Algorithme: SDCA (Stochastic Dual Coordinate Ascent) pour classification multi-classe

    public class AiService : IAiService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private readonly string _modelPath;

        public AiService(IConfiguration configuration)
        {
            _mlContext = new MLContext(seed: 1); // Seed pour reproductibilité
            _modelPath = configuration["MLModels:CategoryPredictionModelPath"]
                ?? "MLModels/CategoryModel.zip";

            // Créer le dossier MLModels s'il n'existe pas
            var directory = Path.GetDirectoryName(_modelPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Charger le modèle s'il existe déjà
            if (File.Exists(_modelPath))
            {
                _model = _mlContext.Model.Load(_modelPath, out _);
            }
        }

     
        /// Entraîne le modèle ML.NET avec les données d'exemple
        /// Pipeline: Featurization (TF-IDF) -> SDCA Multi-class Trainer
     
        public void TrainModel()
        {
            // Si le modèle existe déjà, ne pas réentraîner
            if (_model != null)
            {
                Console.WriteLine("✅ Modèle ML déjà chargé depuis le disque.");
                return;
            }

            Console.WriteLine("🤖 Entraînement du modèle ML.NET en cours...");

            // 1. Charger les données d'entraînement
            var trainingData = TrainingData.GetSampleData();
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // 2. Créer le pipeline de transformation et d'entraînement
            var pipeline = _mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(TransactionInput.Category))
                // Featurization: Convertir le texte en features numériques (TF-IDF)
                .Append(_mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "Features",
                    inputColumnName: nameof(TransactionInput.Description)))
                // Algorithme: SDCA (très rapide et efficace pour classification multi-classe)
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                    labelColumnName: "Label",
                    featureColumnName: "Features"))
                // Convertir les labels numériques en catégories textuelles
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue(
                    outputColumnName: "PredictedLabel"));

            // 3. Entraîner le modèle
            _model = pipeline.Fit(dataView);

            // 4. Sauvegarder le modèle sur disque
            _mlContext.Model.Save(_model, dataView.Schema, _modelPath);

            Console.WriteLine($"✅ Modèle ML.NET entraîné et sauvegardé dans {_modelPath}");
        }

     
        /// Prédit la catégorie d'une transaction basée sur sa description
     
        /// <param name="description">Description de la transaction</param>
        /// <returns>Catégorie prédite</returns>
        public string PredictCategory(string description)
        {
            if (_model == null)
            {
                Console.WriteLine("⚠️ Modèle non entraîné, entraînement en cours...");
                TrainModel();
            }

            if (_model == null)
            {
                return "Autre"; // Fallback si l'entraînement échoue
            }

            // Créer le moteur de prédiction
            var predictionEngine = _mlContext.Model
                .CreatePredictionEngine<TransactionInput, CategoryPrediction>(_model);

            // Faire la prédiction
            var input = new TransactionInput { Description = description };
            var prediction = predictionEngine.Predict(input);

            Console.WriteLine($"🔍 Prédiction: '{description}' -> {prediction.PredictedCategory}");

            return prediction.PredictedCategory;
        }
    }
}