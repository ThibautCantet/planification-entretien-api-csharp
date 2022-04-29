using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.UserCase;
using infra = PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class EntretienRepository : IEntretienRepository
    {
        private readonly IDictionary<Guid, infra.InMemoryEntretien> _entretiens = new Dictionary<Guid, infra.InMemoryEntretien>();
        private readonly object _lock = new object();

        public IEnumerable<Entretien> FindAll()
        {
            foreach (var entretien in _entretiens.Values)
            {
                yield return Entretien.of(entretien.Id, entretien.DateEtHeure, entretien.EmailCandidat, entretien.EmailRecruteur);
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

                _entretiens[entretien.Id] = new infra.InMemoryEntretien(entretien.Id, entretien.DateEtHeure, entretien.EmailCandidat, entretien.EmailRecruteur);
                return entretien;
            }
        }

        public void Clear()
        {
            _entretiens.Clear();
        }
    }
}