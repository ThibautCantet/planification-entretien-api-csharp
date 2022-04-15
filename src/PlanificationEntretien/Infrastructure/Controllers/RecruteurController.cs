using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Test.Controllers;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/recruteur")]
    public class RecruteurController : ControllerBase
    {
        private ICreerRecruteur _creerRecruteur;

        public RecruteurController(ICreerRecruteur creerRecruteur) => _creerRecruteur = creerRecruteur;
        
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
    }
}