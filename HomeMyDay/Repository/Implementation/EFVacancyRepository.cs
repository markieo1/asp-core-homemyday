using System.Collections.Generic;
using HomeMyDay.Models;
using HomeMyDay.Database;
using System;
using System.Linq;
using HomeMyDay.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Repository.Implementation
{
	public class EFVacancyRepository : IVacancyRepository
    {
        private readonly HomeMyDayDbContext _context;

        public EFVacancyRepository(HomeMyDayDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vacancy> Vacancies => _context.Vacancies;

        public Vacancy GetVacancy(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Vacancy vacancy = _context.Vacancies.FirstOrDefault(item => item.Id == id);

            if (vacancy == null)
            {
                throw new KeyNotFoundException($"Vacancie with ID: {id} is not found");
            }

            return vacancy;
        }

        public Task<PaginatedList<Vacancy>> List(int page = 1, int pageSize = 10)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page));
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize));
            }

            IQueryable<Vacancy> vacancies = _context.Vacancies.OrderBy(x => x.Id).AsNoTracking();

            return PaginatedList<Vacancy>.CreateAsync(vacancies, page, pageSize);
        }
    }
}
