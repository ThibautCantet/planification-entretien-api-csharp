using System.Collections.Generic;
using PlanificationEntretien.Infrastructure.Models;

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