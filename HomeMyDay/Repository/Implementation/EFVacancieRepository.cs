using System.Collections.Generic;
using HomeMyDay.Models;
using HomeMyDay.Database;
using System;
using System.Linq;

namespace HomeMyDay.Repository.Implementation
{
	public class EFVacancieRepository : IVacancieRepository
    {
        private readonly HomeMyDayDbContext _context;

        public EFVacancieRepository(HomeMyDayDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Vacancy> Vacancies => _context.Vacancies;

        public Vacancy GetVacancie(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Vacancy vacancie = _context.Vacancies.FirstOrDefault(item => item.Id == id);

            if (vacancie == null)
            {
                throw new KeyNotFoundException($"Vacancie with ID: {id} is not found");
            }

            return vacancie;
        }
    }
}
