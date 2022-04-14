using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.Domain.Ports
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien entretien);
        void Clear();
    }
}