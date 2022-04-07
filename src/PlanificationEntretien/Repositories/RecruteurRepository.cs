using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository
{
    public class RecruteurRepository : IRecruteurRepository
    {
        private readonly IDictionary<Guid, Recruteur> _recruteurs = new Dictionary<Guid, Recruteur>();
        private readonly object _lock = new object();

        public IEnumerable<Recruteur> FindAll()
        {
            return _recruteurs.Values;
        }

        public Recruteur Save(Recruteur recruteur)
        {
            lock (_lock)
            {
                if (recruteur == null)
                {
                    return null;
                }

                _recruteurs[recruteur.Id] = recruteur;
                return recruteur;
            }
        }

        public Recruteur FindById(Guid id)
        {
            return _recruteurs[id];
        }

        public void Clear()
        {
            _recruteurs.Clear();
        }
    }
}