using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien entretien);
        void Clear();
    }
}