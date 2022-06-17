using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.Infrastructure.Repositories.InMemory.Models;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class ListEntretienRepository : IEntretienRepository
    {
        private readonly List<ListEntretien> _entretiens = new();
        private readonly object _lock = new object();

        public IEnumerable<Entretien> FindAll()
        {
            foreach (var entretien in _entretiens)
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

                foreach (var e in _entretiens)
                {
                    if (e.Id == entretien.Id)
                    {
                        _entretiens.Remove(e);
                        break;
                    }
                }
                _entretiens.Add(new ListEntretien(entretien.Id, entretien.DateEtHeure, entretien.EmailCandidat, entretien.EmailRecruteur));

                return entretien;
            }
        }

        public void Clear()
        {
            _entretiens.Clear();
        }
    }
}