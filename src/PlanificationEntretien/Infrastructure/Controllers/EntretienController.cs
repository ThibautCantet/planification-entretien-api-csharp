using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/entretien")]
    public class EntretienController : ControllerBase
    {
        private IPlanifierEntretien _planifierEntretien;
        private IListerEntretiens _listerEntretiens;

        public EntretienController(IPlanifierEntretien planifierEntretien, IListerEntretiens listerEntretiens)
        {
            _planifierEntretien = planifierEntretien;
            _listerEntretiens = listerEntretiens;
        }


        [HttpGet]
        public IActionResult Lister()
        {
            return Ok(_listerEntretiens.Execute());
        }

        [HttpPost]
        public IActionResult Planifier([FromBody] PlanificationDto planificationDto)
        {
            var result = _planifierEntretien.Execute(planificationDto.CandidatId, planificationDto.RecruteurId, planificationDto.DisponibiliteCandidat, planificationDto.DisponibiliteRecruteur);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}