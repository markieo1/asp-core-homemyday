using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Managers
{
	public interface INewspaperManager
	{
		Newspaper GetNewspaper(long id);

		IEnumerable<Newspaper> GetNewspapers();

		bool Subscribe(string email);

		void Unsubscribe(string email);
	}
}
