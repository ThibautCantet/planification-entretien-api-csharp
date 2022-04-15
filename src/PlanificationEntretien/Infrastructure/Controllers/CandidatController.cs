using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;

namespace PlanificationEntretien.Infrastructure.Controllers
{
    [Route("api/candidat")]
    public class CandidatController : ControllerBase
    {
        private ICandidatRepository _candidatRepository;

        public CandidatController(ICandidatRepository candidatRepository) => _candidatRepository = candidatRepository;

        [HttpPost]
        public IActionResult Creer([FromBody] CandidatDto candidat)
        {
            String EMAIL_REGEX = "^[\\w-_.+]*[\\w-_.]@([\\w]+\\.)+[\\w]+[\\w]$";
            MatchCollection mc = Regex.Matches(candidat.Email, EMAIL_REGEX);
            if (mc.Count == 0 
                || String.IsNullOrEmpty(candidat.Language)
                || String.IsNullOrEmpty(candidat.Email)
                || !candidat.Xp.HasValue
                || candidat.Xp < 0)
            {
                return BadRequest();
            }

            var result = _candidatRepository.Save(new Candidat(Guid.NewGuid(), candidat.Language, candidat.Email, candidat.Xp));
            if (result == null)
            {
                return BadRequest();
            }

            return Created("", result);
        }
    }
}