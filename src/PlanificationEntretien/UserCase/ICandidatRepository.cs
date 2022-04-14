using System;
using System.Collections.Generic;
using PlanificationEntretien.Domain;

namespace PlanificationEntretien.UserCase;

public interface ICandidatRepository
{
    void Clear();
    IEnumerable<Candidat> FindAll();
    Candidat Save(Candidat candidat);
    Candidat FindById(Guid id);
}