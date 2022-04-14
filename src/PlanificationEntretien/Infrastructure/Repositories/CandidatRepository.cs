using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.UserCase;
using infra = PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class CandidatRepository : ICandidatRepository
    {
        private readonly IDictionary<Guid, infra.Candidat> _candidats = new Dictionary<Guid, infra.Candidat>();
        private readonly object _lock = new object();

        public IEnumerable<Candidat> FindAll()
        {
            foreach (var candidat in _candidats.Values)
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

                _candidats[candidat.Id] = new infra.Candidat(candidat.Id, candidat.Language, candidat.Email, candidat.Xp);
                return candidat;
            }
        }

        public Candidat FindById(Guid id)
        {
            var candidat = _candidats[id];
            return new Candidat(id, candidat.Language, candidat.Email, candidat.Xp);
        }

        public void Clear()
        {
            _candidats.Clear();
        }
    }
}