using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;

namespace PlanificationEntretien.Controllers
{
    [Route("api/candidat")]
    public class CandidatController : ControllerBase
    {
        private ICandidatRepository _candidatRepository;

        public CandidatController(ICandidatRepository candidatRepository) => _candidatRepository = candidatRepository;
        
        [HttpPost]
        public IActionResult Planifier([FromBody] Candidat candidat)
        {
            var result = _candidatRepository.Save(candidat);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}