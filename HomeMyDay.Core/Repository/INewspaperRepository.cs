using HomeMyDay.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeMyDay.Core.Repository
{
	public interface INewspaperRepository
	{
		IEnumerable<Newspaper> Newspapers { get; }

		/// <summary>
		/// Add a user to the mailing list.
		/// </summary>
		/// <param name="email">The email of the user to subscribe</param>
		/// <returns></returns>
		bool Subscribe(string email);

		/// <summary>
		/// Remove a user from the mailing list.
		/// </summary>
		/// <param name="email">The email of the user to unsubscribe</param>
		/// <returns></returns>
		Task Unsubscribe(string email);
	}
}
