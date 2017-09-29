using HomeMyDay.Database;
using HomeMyDay.Models;

namespace HomeMyDay.Repository.Implementation
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
		    _context.Newspapers.Add(new Newspaper {Email = email});
		    if (_context.SaveChanges() > 0)
			    isSaved = true;

		    return isSaved;
	    }
    }
}
