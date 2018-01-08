using System;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Infrastructure.Repository
{
	public class EFNewspaperRepository : INewspaperRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFNewspaperRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Newspaper> Newspapers => _context.Newspapers;

		public bool Subscribe(string email)
		{
			bool isSaved = false;

			if (string.IsNullOrWhiteSpace(email))
			{
				throw new ArgumentNullException(nameof(email));
			}

			try
			{
				string subscribeEmail = email.Trim();
				_context.Newspapers.Add(new Newspaper {Email = subscribeEmail});
				if (_context.SaveChanges() > 0)
				{
					isSaved = true;
				}
			}
			catch (Exception)
			{
				isSaved = false;
			}

			return isSaved;
		}

		public async Task Unsubscribe(string email)
		{
			Newspaper newspaper = _context.Newspapers.FirstOrDefault(n => n.Email == email);

			if(newspaper == null)
			{
				throw new KeyNotFoundException($"Newspaper with email {email} not found");
			}

			_context.Newspapers.Remove(newspaper);

			await _context.SaveChangesAsync();
		}
	}
}
