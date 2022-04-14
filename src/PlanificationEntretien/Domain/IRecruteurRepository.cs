using System;
using System.Collections.Generic;

namespace PlanificationEntretien.Domain;

public interface IRecruteurRepository
{
    void Clear();
    IEnumerable<Recruteur> FindAll();
    Recruteur Save(Recruteur recruteur);
    Recruteur FindById(Guid id);
}