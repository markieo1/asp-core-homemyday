using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Core.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Infrastructure.Repository
{
    public class EFCountryRepository : ICountryRepository
    {
		private readonly HomeMyDayDbContext _context;

		public IEnumerable<Country> Countries => _context.Countries;

		public EFCountryRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public Country GetCountry(long id)
		{
			var country = _context.Countries.First(c => c.Id == id);

			return country;
		}

		public async Task Save(Country country)
		{
			if (country == null)
			{
				throw new ArgumentNullException(nameof(country));
			}

			if (country.Id <= 0)
			{
				// We are creating a new one
				// Only need to adjust the id to be 0 and save it in the db.
				_context.Countries.Add(country);
			}
			else
			{
				// Get the tracked accommodation using the ID
				EntityEntry<Country> entityEntry = _context.Entry(country);
				entityEntry.State = EntityState.Modified;
			}

			await _context.SaveChangesAsync();
		}

		public async Task Delete(long id)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}

			Country country = _context.Countries.SingleOrDefault(a => a.Id == id);

			if (country == null)
			{
				throw new ArgumentNullException(nameof(id), $"Country with ID: {id} not found!");
			}

			_context.Countries.Remove(country);

			await _context.SaveChangesAsync();
		}
	}
}
