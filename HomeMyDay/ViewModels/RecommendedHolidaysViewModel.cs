using System.Collections.Generic;
using HomeMyDay.Models;

namespace HomeMyDay.ViewModels
{
    public class RecommendedHolidaysViewModel
    {
		public IEnumerable<Booking> Holidays { get; set; }
    }
}
