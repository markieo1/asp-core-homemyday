using System;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;

namespace HomeMyDay.Infrastructure.Repository
{
	public class EFNewspaperRepository : INewspaperRepository
	{
		private readonly HomeMyDayDbContext _context;

		public EFNewspaperRepository(HomeMyDayDbContext context)
		{
			_context = context;
		}

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
	}
}
