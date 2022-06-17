using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.Infrastructure.Repositories.InMemory.Models;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class ListCandidatRepository : ICandidatRepository
    {
        private readonly List<ListCandidat> _candidats = new();
        private readonly object _lock = new object();

        public IEnumerable<Candidat> FindAll()
        {
            foreach (var candidat in _candidats)
            {
                yield return new Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.Xp);
            }
        }

        public Candidat Save(Candidat candidat)
        {
            lock (_lock)
            {
                if (candidat == null)
                {
                    return null;
                }

                foreach (var e in _candidats)
                {
                    if (e.Id == candidat.Id)
                    {
                        _candidats.Remove(e);
                        break;
                    }
                }
                _candidats.Add(new ListCandidat(candidat.Id, candidat.Language, candidat.Email, candidat.Xp));

                return candidat;
            }
        }

        public Candidat FindById(Guid id)
        {
            foreach (var candidat in _candidats)
            {
                if (candidat.Id == id)
                {
                    return new Candidat(id, candidat.Language, candidat.Email, candidat.Xp);
                }
            }

            return null;
        }

        public void Clear()
        {
            _candidats.Clear();
        }
    }
}