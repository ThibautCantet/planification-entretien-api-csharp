using System;
using MasterClass.WebApi;

namespace PlanificationEntretien
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
                var entretien = new Entretien(disponibiliteCandidat, candidat.Email, recruteur.Email);
                _emailService.SendToCandidat(candidat.Email);
                _emailService.SendToRecruteur(recruteur.Email);

                return _entretienRepository.Save(entretien);
            }

            return null;
        }
    }
}