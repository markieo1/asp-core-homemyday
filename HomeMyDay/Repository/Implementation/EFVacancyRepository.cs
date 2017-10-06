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

        public Vacancy DeleteVacancy(long vacancyId)
        {
            Vacancy dbEntry = _context.Vacancies.FirstOrDefault(a => a.Id == vacancyId);

            if (dbEntry != null)
            {
                _context.Vacancies.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveVacancy(string JobTitle, string Company, string City, string AboutVacancy, string AboutFunction, string JobRequirements, string WeOffer)
        {
            if (string.IsNullOrEmpty(JobTitle))
            {
                // do nothing
            }
            else
            {
                _context.Add(new Vacancy() { JobTitle = JobTitle, Company = Company, City = City, AboutVacancy = AboutVacancy, AboutFunction = AboutFunction, JobRequirements = JobRequirements, WeOffer = WeOffer });
            }
            _context.SaveChanges();
        }

        public void UpdateVacancy(Vacancy vacancy)
        {
            Vacancy dbEntry = _context.Vacancies.FirstOrDefault(s => s.Id == vacancy.Id);
            if (dbEntry != null)
            {
                dbEntry.JobTitle = vacancy.JobTitle;
                dbEntry.Company = vacancy.Company;
                dbEntry.City = vacancy.City;
                dbEntry.AboutVacancy = vacancy.AboutVacancy;
                dbEntry.AboutFunction = vacancy.AboutFunction;
                dbEntry.JobRequirements = vacancy.JobRequirements;
                dbEntry.WeOffer = vacancy.WeOffer;
            }
            _context.SaveChanges();
        }
    }
}