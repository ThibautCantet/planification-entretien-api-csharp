using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.Domain.Ports;

public interface IListerEntretiens
{
    IEnumerable<Entretien> Execute();
}