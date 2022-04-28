using System;
using System.Collections.Generic;
using PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.UserCase
{
    public class PlanifierEntretien : IPlanifierEntretien
    {
        private readonly IEntretienRepository _entretienRepository;
        private readonly IEmailService _emailService;
        private readonly IRecruteurRepository _recruteurRepository;
        private readonly ICandidatRepository _candidatRepository;

        public PlanifierEntretien(IEntretienRepository entretienRepository, IEmailService emailService,
            IRecruteurRepository recruteurRepository, ICandidatRepository candidatRepository)
        {
            _entretienRepository = entretienRepository;
            _emailService = emailService;
            _recruteurRepository = recruteurRepository;
            _candidatRepository = candidatRepository;
        }

        public Entretien Execute(Guid candidatId, Guid recruteurId, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur)
        {
            var recruteur = _recruteurRepository.FindById(recruteurId);
            var candidat = _candidatRepository.FindById(candidatId);
            
            if (disponibiliteCandidat.Date.Equals(disponibiliteRecruteur.Date) &&
                candidat.Language.Equals(recruteur.Language)
                && recruteur.Xp > candidat.Xp)
            {
                var entretien = new Entretien(Guid.NewGuid(), disponibiliteCandidat, candidat.Email, recruteur.Email);
                _emailService.SendToCandidat(candidat.Email);
                _emailService.SendToRecruteur(recruteur.Email);

                return _entretienRepository.Save(entretien);
            }

            return null;
        }

    }
}