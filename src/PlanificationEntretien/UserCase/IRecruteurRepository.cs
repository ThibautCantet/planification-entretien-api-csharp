using System;
using System.Collections.Generic;
using PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.UserCase;

public interface IRecruteurRepository
{
    void Clear();
    IEnumerable<Recruteur> FindAll();
    Recruteur Save(Recruteur recruteur);
    Recruteur FindById(Guid id);
}