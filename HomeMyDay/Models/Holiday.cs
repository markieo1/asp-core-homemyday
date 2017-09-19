using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Holiday
    {
		public Accommodation Accommodation { get; set; }
		public DateTime DepartureDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public int NrPersons { get; set; }
    }
}
