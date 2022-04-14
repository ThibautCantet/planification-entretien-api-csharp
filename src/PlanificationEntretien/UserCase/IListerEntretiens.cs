using System.Collections.Generic;
using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase;

public interface IListerEntretiens
{
    IEnumerable<Entretien> Execute();
}