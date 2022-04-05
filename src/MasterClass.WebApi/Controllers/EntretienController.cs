using Microsoft.AspNetCore.Mvc;

namespace MasterClass.WebApi.Controllers
{
    [Route("api/entretien")]
    public class EntretienController : ControllerBase
    {
        private IEntretienService _entretienService;

        public EntretienController(IEntretienService entretienService) => _entretienService = entretienService;
        
        [HttpPost]
        public IActionResult Planifier([FromBody] PlanificationDto planificationDto)
        {
            var result = _entretienService.planifier(planificationDto.Candidat, planificationDto.Recruteur, planificationDto.DisponibiliteCandidat, planificationDto.DisponibiliteRecruteur);
            return Created("", result);
        }
    }
}