using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class VacancyManager : IVacancyManager
	{
		private readonly IVacancyRepository _vacancyRepository;

		public VacancyManager(IVacancyRepository vacancyRepository)
		{
			_vacancyRepository = vacancyRepository;
		}

		public Task<PaginatedList<Vacancy>> GetVacancyPaginatedList(int? page, int? pageSize)
		{
			return _vacancyRepository.List(page ?? 1, pageSize ?? 5);
		}

		public Task Save(Vacancy vacancy)
		{
			return _vacancyRepository.SaveVacancy(vacancy);
		}

		public Task Delete(long id)
		{
			return _vacancyRepository.DeleteVacancy(id);
		}

		public IEnumerable<Vacancy> GetVacancies()
		{
			return _vacancyRepository.Vacancies;
		}

		public Vacancy GetVacancy(long id)
		{
			if (id <= 0)
			{
				return new Vacancy();
			}

			return _vacancyRepository.GetVacancy(id);
		}
	}
}
