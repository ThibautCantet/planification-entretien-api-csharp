using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository
{
    public class EntretienRepository : IEntretienRepository
    {
        private readonly IDictionary<Guid, Entretien> _entretiens = new Dictionary<Guid, Entretien>();
        private readonly object _lock = new object();

        public IEnumerable<Entretien> FindAll()
        {
            return _entretiens.Values;
        }

        public Entretien Save(Entretien entretien)
        {
            lock (_lock)
            {
                if (entretien == null)
                {
                    return null;
                }

                _entretiens[entretien.Id] = entretien;
                return entretien;
            }
        }

        public void Clear()
        {
            _entretiens.Clear();
        }
    }
}