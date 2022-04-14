using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain.Entities;

namespace PlanificationEntretien.Domain.Ports;

public interface ICandidatRepository
{
    void Clear();
    IEnumerable<Candidat> FindAll();
    Candidat Save(Candidat candidat);
    Candidat FindById(Guid id);
}