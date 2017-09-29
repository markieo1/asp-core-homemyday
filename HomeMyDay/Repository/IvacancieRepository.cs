using HomeMyDay.Models;
using System.Collections.Generic;

namespace HomeMyDay.Repository
{
    public interface IVacancieRepository
    {
        IEnumerable<Vacancie> GetVacancies { get; }

        Vacancie GetVacancie(long id);
    }
}
