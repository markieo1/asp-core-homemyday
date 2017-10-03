using System.Collections.Generic;
using HomeMyDay.Models;
using HomeMyDay.Database;
using System;
using System.Linq;

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
    }
}
