using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.UserCase;
using infra = PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class EntretienRepository : IEntretienRepository
    {
        private readonly IDictionary<Guid, infra.Entretien> _entretiens = new Dictionary<Guid, infra.Entretien>();
        private readonly object _lock = new object();

        public IEnumerable<Entretien> FindAll()
        {
            foreach (var entretien in _entretiens.Values)
            {
                yield return new Entretien(entretien.Id, entretien.DateEtHeure, entretien.EmailCandidat, entretien.EmailRecruteur);
            }
        }

        public Entretien Save(Entretien entretien)
        {
            lock (_lock)
            {
                if (entretien == null)
                {
                    return null;
                }

                _entretiens[entretien.Id] = new infra.Entretien(entretien.Id, entretien.DateEtHeure, entretien.EmailCandidat, entretien.EmailRecruteur);
                return entretien;
            }
        }

        public void Clear()
        {
            _entretiens.Clear();
        }
    }
}