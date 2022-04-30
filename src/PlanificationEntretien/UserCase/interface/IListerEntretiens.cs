using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.UserCase;

public interface IListerEntretiens
{
    IEnumerable<Entretien> Execute();
}