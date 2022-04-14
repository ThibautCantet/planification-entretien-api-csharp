using System.Collections.Generic;

namespace PlanificationEntretien.Domain
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien entretien);
        void Clear();
    }
}