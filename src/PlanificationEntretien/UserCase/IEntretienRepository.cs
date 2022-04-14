using System.Collections.Generic;
using PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.UserCase
{
    public interface IEntretienRepository
    {
        IEnumerable<Entretien> FindAll();
        Entretien Save(Entretien entretien);
        void Clear();
    }
}