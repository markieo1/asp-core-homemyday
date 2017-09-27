using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Database
{
    public class HolidayDbContext : DbContext
	{
		/// <summary>
		/// Required constructor for entity framework. Passes options to parent class constructor.
		/// </summary>
		/// <param name="options">The DB options to pass to the base class.</param>
		public HolidayDbContext(DbContextOptions<HolidayDbContext> options) : base(options)
		{

		}

		/// <summary>
		/// The Holidays that have been saved.
		/// </summary>
		public DbSet<Holiday> Holidays { get; set; }

		/// <summary>
		/// The Bookings that have been saved.
		/// </summary>
		public DbSet<Booking> Bookings { get; set; }
		
		/// <summary>
		/// The Accommodations that have been saved.
		/// </summary>
		public DbSet<Accommodation> Accommodations { get; set; }

        /// <summary>
		/// The Reviews that have been saved.
		/// </summary>
        public DbSet<Review> Reviews { get; set; }
    }
}
