using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Test.Controllers;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/recruteur")]
    public class RecruteurController : ControllerBase
    {
        private ICreerRecruteur _creerRecruteur;
        private IListerRecruteursExperimentes _listerRecruteursExperimentes;

        public RecruteurController(ICreerRecruteur creerRecruteur,
            IListerRecruteursExperimentes listerRecruteursExperimentes)
        {
            _creerRecruteur = creerRecruteur;
            _listerRecruteursExperimentes = listerRecruteursExperimentes;
        }

        [HttpPost]
        public IActionResult Creer([FromBody] RecruteurDto recruteur)
        {
            var result = _creerRecruteur.Execute(recruteur.Language, recruteur.Email, recruteur.Xp);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }

        [HttpGet]
        public IActionResult ListerExperimentes()
        {
            return Ok(_listerRecruteursExperimentes.Execute()
                .Select(result => new RecruteurExperimenteDto(result.Id, result.Competence, result.Email)));
        }
    }
}