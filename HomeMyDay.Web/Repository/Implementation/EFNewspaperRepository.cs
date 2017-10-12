using System;
using HomeMyDay.Web.Database;
using HomeMyDay.Core.Models;

namespace HomeMyDay.Web.Repository.Implementation
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
