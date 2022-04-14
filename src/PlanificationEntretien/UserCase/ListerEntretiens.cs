using System.Collections.Generic;
using PlanificationEntretien.Domain;
using PlanificationEntretien.Domain.Entities;
using PlanificationEntretien.Domain.Ports;

namespace PlanificationEntretien.UserCase;

public class ListerEntretiens : IListerEntretiens
{
    
    private readonly IEntretienRepository _entretienRepository;

    public ListerEntretiens(IEntretienRepository entretienRepository) =>_entretienRepository = entretienRepository;

    public IEnumerable<Entretien> Execute()
    {
        return _entretienRepository.FindAll();
    }
}