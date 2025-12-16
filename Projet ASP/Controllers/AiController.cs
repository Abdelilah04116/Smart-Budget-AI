using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_ASP.Services;

namespace Projet_ASP.Controllers
{
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

    public class PredictionRequest
    {
        public string Description { get; set; } = string.Empty;
    }
}