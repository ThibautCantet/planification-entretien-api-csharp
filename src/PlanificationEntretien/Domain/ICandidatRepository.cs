using System;
using System.Collections.Generic;

namespace PlanificationEntretien.Domain;

public interface ICandidatRepository
{
    void Clear();
    IEnumerable<Candidat> FindAll();
    Candidat Save(Candidat candidat);
    Candidat FindById(Guid id);
}