using System;
using MasterClass.WebApi;
using PlanificationEntretien.Models;
using PlanificationEntretien.Repository;

namespace PlanificationEntretien.Services
{
    public class EntretienService : IEntretienService
    {
        private IEntretienRepository _entretienRepository;
        private IEmailService _emailService;

        public EntretienService(IEntretienRepository entretienRepository, IEmailService emailService)
        {
            _entretienRepository = entretienRepository;
            _emailService = emailService;
        }

        public Entretien Planifier(Candidat candidat, Recruteur recruteur, DateTime disponibiliteCandidat,
            DateTime disponibiliteRecruteur)
        {
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