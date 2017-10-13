using HomeMyDay.Core.Repository;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class NewspaperManager : INewspaperManager
    {
	    private INewspaperRepository _newspaperRepository;

	    public NewspaperManager(INewspaperRepository newspaperRepository)
	    {
		    _newspaperRepository = newspaperRepository;
	    }	  

	    public bool Subscribe(string email)
	    {
		    return _newspaperRepository.Subscribe(email);
	    }
    }
}
