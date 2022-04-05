using System.Collections.Generic;
using PlanificationEntretien;

namespace MasterClass.WebApi
{
    public class EntretienRepository : IEntretienRepository
    {
        public IEnumerable<Entretien> FindAll()
        {
            return null;
        }

        public Entretien Save(Entretien entretien)
        {
            return entretien;
        }
    }
}