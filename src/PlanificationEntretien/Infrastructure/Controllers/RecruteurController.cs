using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Test.Controllers;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/recruteur")]
    public class RecruteurController : ControllerBase
    {
        private IRecruteurRepository _recruteurRepository;

        public RecruteurController(IRecruteurRepository recruteurRepository) => _recruteurRepository = recruteurRepository;
        
        [HttpPost]
        public IActionResult Creer([FromBody] RecruteurDto recruteur)
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
            
            var result = _recruteurRepository.Save(new Recruteur(Guid.NewGuid(), recruteur.Language, recruteur.Email, recruteur.Xp));
            if (result == null)
            {
                return BadRequest();
            }
            return Created("", result);
        }
    }
}