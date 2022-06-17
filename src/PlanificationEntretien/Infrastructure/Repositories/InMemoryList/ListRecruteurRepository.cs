using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;
using PlanificationEntretien.Infrastructure.Repositories.InMemoryList.Models;

namespace PlanificationEntretien.Infrastructure.Repositories
{
    public class ListRecruteurRepository : IRecruteurRepository
    {
        private readonly List<ListRecruteur> _recruteurs = new();
        private readonly object _lock = new object();

        public IEnumerable<Recruteur> FindAll()
        {
            foreach (var recruteur in _recruteurs)
            {
                yield return new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.Xp);
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

                foreach (var r in _recruteurs)
                {
                    if (r.Id == recruteur.Id)
                    {
                        _recruteurs.Remove(r);
                    }
                }
                _recruteurs.Add(new ListRecruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.Xp));
                return recruteur;
            }
        }

        public Recruteur FindById(Guid id)
        {
            foreach (var recruteur in _recruteurs)
            {
                if (recruteur.Id == id)
                {
                    return new Recruteur(recruteur.Id, recruteur.Language, recruteur.Email, recruteur.Xp);
                }
            }

            return null;
        }

        public void Clear()
        {
            _recruteurs.Clear();
        }
    }
}