using System.Collections.Generic;
using HomeMyDay.Models;
using HomeMyDay.Database;
using System;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Vacancie> Vacancies => _context.Vacancies;

        public Vacancie Vacancie(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Vacancie vacancie = _context.Vacancies.FirstOrDefault(item => item.Id == id);

            if (vacancie == null)
            {
                throw new KeyNotFoundException($"Vacancie with ID: {id} is not found");
            }

            return vacancie;
        }
    }
}
