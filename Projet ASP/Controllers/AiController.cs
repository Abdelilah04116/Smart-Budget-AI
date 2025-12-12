using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBudgetAI.Services;
using System;

namespace SmartBudgetAI.Controllers
{
    /// Contrôleur API pour les fonctionnalités d'IA
    /// Fournit un endpoint AJAX pour prédire les catégories
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;

        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

        /// API: Prédit la catégorie d'une transaction
        /// POST: /api/ai/predict-category
        /// Body: { "description": "Achat supermarché" }
        [HttpPost("predict-category")]
        public IActionResult PredictCategory([FromBody] PredictionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return BadRequest(new { error = "La description est requise" });
            }

            try
            {
                var predictedCategory = _aiService.PredictCategory(request.Description);
                return Ok(new { category = predictedCategory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Erreur de prédiction: {ex.Message}" });
            }
        }
    }

    /// DTO pour la requête de prédiction
    public class PredictionRequest
    {
        public string Description { get; set; } = string.Empty;
    }
}