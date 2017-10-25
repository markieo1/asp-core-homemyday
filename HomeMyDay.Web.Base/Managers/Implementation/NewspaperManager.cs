using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class NewspaperManager : INewspaperManager
    {
	    private INewspaperRepository _newspaperRepository;

		public NewspaperManager(INewspaperRepository newspaperRepository)
		{
			_newspaperRepository = newspaperRepository;
		}

		public Newspaper GetNewspaper(long id)
		{
			var newspaper = _newspaperRepository.Newspapers.FirstOrDefault(n => n.Id == id);

			if(newspaper == null)
			{
				throw new KeyNotFoundException($"Newspaper with ID {id} not found");
			}

			return newspaper;
		}

		public IEnumerable<Newspaper> GetNewspapers()
		{
			return _newspaperRepository.Newspapers;
		}

	    public bool Subscribe(string email)
	    {
		    return _newspaperRepository.Subscribe(email);
	    }

		public void Unsubscribe(string email)
		{
			_newspaperRepository.Unsubscribe(email);
		}
    }
}
