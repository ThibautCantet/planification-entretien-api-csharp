using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository;

public interface IRecruteurRepository
{
    void Clear();
    IEnumerable<Recruteur> FindAll();
    Recruteur Save(Recruteur recruteur);
}