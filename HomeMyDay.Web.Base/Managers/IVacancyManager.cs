using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Base.Managers
{
	public interface IVacancyManager
    {
	    Task<PaginatedList<Vacancy>> GetVacancyPaginatedList(int? page, int? pageSize);

	    Task Save(Vacancy vacancy);

	    Task Delete(long id);

	    IEnumerable<Vacancy> GetVacancies();
		
		Vacancy GetVacancy(long id);
    }
}
