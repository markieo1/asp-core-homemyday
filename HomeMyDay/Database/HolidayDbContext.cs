using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Database
{
    public class HolidayDbContext : DbContext
    {
		/// <summary>
		/// The Holidays that have been saved.
		/// </summary>
		public DbSet<Holiday> Holidays { get; set; }
		
		/// <summary>
		/// The Accommodations that have been saved.
		/// </summary>
		public DbSet<Accommodation> Accommodations { get; set; }
    }
}
