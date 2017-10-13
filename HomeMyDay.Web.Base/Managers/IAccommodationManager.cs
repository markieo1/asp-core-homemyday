using HomeMyDay.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMyDay.Web.Base.Managers
{
    public interface IAccommodationManager
    {
	    Task<PaginatedList<Accommodation>> GetAccommodationPaginatedList(int? page, int? pageSize);
			
		

	}
}
