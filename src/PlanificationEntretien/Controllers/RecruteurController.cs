using System;
using System.Text.RegularExpressions;
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
            String EMAIL_REGEX = "^[\\w-_.+]*[\\w-_.]@([\\w]+\\.)+[\\w]+[\\w]$";
            MatchCollection mc = Regex.Matches(recruteur.Email, EMAIL_REGEX);
            if (mc.Count == 0 
                || String.IsNullOrEmpty(recruteur.Language)
                || String.IsNullOrEmpty(recruteur.Email)
                || !recruteur.Xp.HasValue
                || recruteur.Xp < 0)
            {
                return BadRequest();
            }
            
            var result = _recruteurRepository.Save(recruteur);
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}