using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
    public interface ICountryRepository
    {
		/// <summary>
		/// An IEnumerable of all countries.
		/// </summary>
		IEnumerable<Country> Countries { get; }
    }
}
