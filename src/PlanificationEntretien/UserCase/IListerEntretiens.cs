using System.Collections.Generic;
using PlanificationEntretien.Infrastructure.Models;

namespace PlanificationEntretien.UserCase;

public interface IListerEntretiens
{
    IEnumerable<Entretien> Execute();
}