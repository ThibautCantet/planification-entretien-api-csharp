using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.Infrastructure.Repositories.InMemory.Models;
using PlanificationEntretien.UserCase;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class RecruteurRepository : IRecruteurRepository
    {
        private readonly IDictionary<Guid, InMemoryRecruteur> _recruteurs = new Dictionary<Guid, InMemoryRecruteur>();
        private readonly object _lock = new object();

        public IEnumerable<Recruteur> FindAll()
        {
            foreach (var candidat in _recruteurs.Values)
            {
                yield return new Recruteur(candidat.Id, candidat.Language, candidat.Email, candidat.Xp);
            }
        }

        public Recruteur Save(Recruteur recruteur)
        {
            lock (_lock)
            {
                if (recruteur == null)
                {
                    return null;
                }

                _recruteurs[recruteur.Id] = new InMemoryRecruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.Xp);
                return recruteur;
            }
        }

        public Recruteur FindById(Guid id)
        {
            var recruteur = _recruteurs[id];
            return new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.Xp);
        }

        public void Clear()
        {
            _recruteurs.Clear();
        }
    }
}