using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;

namespace PlanificationEntretien.Controllers
{
    [Route("api/recruteur")]
    public class RecruteurController : ControllerBase
    {
        private IRecruteurRepository _recruteurRepository;

        public RecruteurController(IRecruteurRepository recruteurRepository) => _recruteurRepository = recruteurRepository;
        
        [HttpPost]
        public IActionResult Creer([FromBody] Recruteur recruteur)
        {
            var result = _recruteurRepository.Save(recruteur);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}