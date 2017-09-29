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

        public IEnumerable<Vacancie> GetVacancies => _context.Jobs;

        public Vacancie GetVacancie(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Vacancie vacancie = _context.Jobs.FirstOrDefault(a => a.Id == id);

            if (vacancie == null)
            {
                throw new KeyNotFoundException($"Accommodation with ID: {id} is not found");
            }

            return vacancie;
        }
    }
}
