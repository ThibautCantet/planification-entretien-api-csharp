using System.Collections.Generic;
using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien entretien);
        void Clear();
    }
}