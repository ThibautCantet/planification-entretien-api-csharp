using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Services;

namespace PlanificationEntretien.Controllers
{
    [Route("api/entretien")]
    public class EntretienController : ControllerBase
    {
        private IEntretienService _entretienService;

        public EntretienController(IEntretienService entretienService) => _entretienService = entretienService;
        
        [HttpPost]
        public IActionResult Planifier([FromBody] PlanificationDto planificationDto)
        {
            var result = _entretienService.Planifier(planificationDto.CandidatId, planificationDto.RecruteurId, planificationDto.DisponibiliteCandidat, planificationDto.DisponibiliteRecruteur);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}