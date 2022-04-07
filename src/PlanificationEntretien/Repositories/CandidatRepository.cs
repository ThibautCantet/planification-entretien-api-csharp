using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository
{
    public class CandidatRepository : ICandidatRepository
    {
        private readonly IDictionary<Guid, Candidat> _candidats = new Dictionary<Guid, Candidat>();
        private readonly object _lock = new object();

        public IEnumerable<Candidat> FindAll()
        {
            return _candidats.Values;
        }

        public Candidat Save(Candidat candidat)
        {
            lock (_lock)
            {
                if (candidat == null)
                {
                    return null;
                }

                _candidats[candidat.Id] = candidat;
                return candidat;
            }
        }

        public Candidat FindById(Guid id)
        {
            return _candidats[id];
        }

        public void Clear()
        {
            _candidats.Clear();
        }
    }
}