using System.Collections.Generic;
using HomeMyDay.Models;
using HomeMyDay.Database;
using System;
using System.Linq;
using HomeMyDay.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task DeleteVacancy(long vacancyId)
        {
            if (vacancyId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vacancyId));
            }

            Vacancy vacancy = await _context.Vacancies.SingleOrDefaultAsync(a => a.Id == vacancyId);

            if (vacancy == null)
            {
                throw new ArgumentNullException(nameof(vacancyId), $"Accommodation with ID: {vacancyId} not found!");
            }

            _context.Vacancies.Remove(vacancy);

            await _context.SaveChangesAsync();
        }

        public async Task SaveVacancy(Vacancy vacancy)
        {
            if (vacancy == null)
            {
                throw new ArgumentNullException(nameof(vacancy));
            }

            if (vacancy.Id <= 0)
            {
                // We are creating a new one
                // Only need to adjust the id to be 0 and save it in the db.
                await _context.Vacancies.AddAsync(vacancy);
            }
            else
            {
                // Get the tracked accommodation using the ID
                EntityEntry<Vacancy> entityEntry = _context.Entry(vacancy);
                entityEntry.State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}