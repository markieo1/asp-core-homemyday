using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Repository
{
    public interface ISupriseRepository
    {
		/// <summary>
		/// Gets the latest suprise.
		/// </summary>
		Suprise GetLastSuprise();
	}
}
