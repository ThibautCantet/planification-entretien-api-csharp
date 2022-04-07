using System;
using System.Collections.Generic;
using PlanificationEntretien.Models;

namespace PlanificationEntretien.Repository;

public interface ICandidatRepository
{
    void Clear();
    IEnumerable<Candidat> FindAll();
    Candidat Save(Candidat candidat);
    Candidat FindById(Guid id);
}