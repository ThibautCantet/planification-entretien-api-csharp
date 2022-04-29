using System;

namespace PlanificationEntretien.Domain.Entities
{
    public class Entretien : IComparable<Entretien>
    {
        private readonly Candidat _candidat;
        private readonly Recruteur _recruteur;
        public Guid Id { get; }
        public string EmailCandidat { get; private set; }
        public string EmailRecruteur { get; private set; }
        public DateTime DateEtHeure { get; private set; }
        
        public Entretien(Candidat candidat, Recruteur recruteur)
        {
            _candidat = candidat;
            _recruteur = recruteur;
        }

        private Entretien(Guid guid, DateTime horaire, string emailCandidat, string emailRecruteur)
        {
            Id = guid;
            DateEtHeure = horaire;
            EmailCandidat = emailCandidat;
            EmailRecruteur = emailRecruteur;
        }

        public bool Planifier(DateTime disponibiliteCandidat, DateTime disponibiliteRecruteur)
        {
            bool planifiable = disponibiliteCandidat.Date.Equals(disponibiliteRecruteur.Date) &&
                   _recruteur.PeutRecruter(_candidat);
            if (planifiable)
            {
                EmailCandidat = _candidat.Email;
                EmailRecruteur = _recruteur.Email;
                DateEtHeure = disponibiliteCandidat;
            }

            return planifiable;

        }

        public static Entretien of(Guid newGuid, DateTime horaire, string emailCandidat, string emailRecruteur)
        {
            return new Entretien(newGuid, horaire, emailCandidat, emailRecruteur);
        }

        public int CompareTo(Entretien other)
        {
            if (Id == other.Id)
            {
                return 0;
            }

            return -1;
        }
    }
}