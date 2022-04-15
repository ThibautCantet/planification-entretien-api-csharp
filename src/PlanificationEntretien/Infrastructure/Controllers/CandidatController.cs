using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/candidat")]
    public class CandidatController : ControllerBase
    {
        private ICreerCandidat _creerCandidat;

        public CandidatController(ICreerCandidat creerCandidat) => _creerCandidat = creerCandidat;

        [HttpPost]
        public IActionResult Creer([FromBody] CandidatDto candidat)
        {
            var result = _creerCandidat.Execute(candidat.Language, candidat.Email, candidat.Xp);
            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }
    }
}