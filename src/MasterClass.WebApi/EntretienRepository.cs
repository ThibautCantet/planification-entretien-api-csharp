using System;
using System.Collections.Generic;
using PlanificationEntretien;

namespace MasterClass.WebApi
{
    public class EntretienRepository : IEntretienRepository
    {
        private readonly IDictionary<Guid, Entretien> _candidates = new Dictionary<Guid, Entretien>();
        private readonly object _lock = new object();

        public IEnumerable<Entretien> FindAll()
        {
            return _candidates.Values;
        }

        public Entretien Save(Entretien entretien)
        {
            lock (_lock)
            {
                if (entretien == null)
                {
                    return null;
                }

                _candidates[entretien.Id] = entretien;
                return entretien;
            }
        }
    }
}