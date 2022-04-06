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
            var result = _entretienService.Planifier(planificationDto.Candidat, planificationDto.Recruteur, planificationDto.DisponibiliteCandidat, planificationDto.DisponibiliteRecruteur);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}