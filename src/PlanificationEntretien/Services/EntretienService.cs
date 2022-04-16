using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;

namespace PlanificationEntretien.Services
{
    public class EntretienService : IEntretienService
    {
        private readonly IEntretienRepository _entretienRepository;
        private readonly IEmailService _emailService;
        private readonly IRecruteurRepository _recruteurRepository;
        private readonly ICandidatRepository _candidatRepository;

        public EntretienService(IEntretienRepository entretienRepository, IEmailService emailService,
            IRecruteurRepository recruteurRepository, ICandidatRepository candidatRepository)
        {
            _entretienRepository = entretienRepository;
            _emailService = emailService;
            _recruteurRepository = recruteurRepository;
            _candidatRepository = candidatRepository;
        }

        public Entretien Planifier(Guid candidatId, Guid recruteurId, DateTime disponibiliteCandidat,
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

        public IEnumerable<Entretien> Lister()
        {
            return _entretienRepository.FindAll();
        }
    }
}