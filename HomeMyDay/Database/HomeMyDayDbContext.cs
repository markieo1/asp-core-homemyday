using HomeMyDay.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeMyDay.Database
{
	public class HomeMyDayDbContext : DbContext
	{
		/// <summary>
		/// Required constructor for entity framework. Passes options to parent class constructor.
		/// </summary>
		/// <param name="options">The DB options to pass to the base class.</param>
		public HomeMyDayDbContext(DbContextOptions<HomeMyDayDbContext> options) : base(options)
		{

		}

		/// <summary>
		/// The Bookings that have been saved.
		/// </summary>
		public DbSet<Booking> Bookings { get; set; }

		/// <summary>
		/// The Accommodations that have been saved.
		/// </summary>
		public DbSet<Accommodation> Accommodations { get; set; }

		/// <summary>
		/// Gets or sets the media objects.
		/// </summary>
		public DbSet<MediaObject> MediaObjects { get; set; }

		/// <summary>
		/// The Countries that have been saved.
		/// </summary>
		public DbSet<Country> Countries { get; set; }

		/// <summary>
		/// Get or set the Suprise Model Content
		/// </summary>
		public DbSet<Page> Page { get; set; }

		/// <summary>
		/// Gets or sets the newspaper objects.
		/// </summary>
		public DbSet<Newspaper> Newspapers { get; set; }

        /// <summary>
		/// The Vacancies that have been saved.
		/// </summary>
        public DbSet<Vacancy> Vacancies { get; set; }

        /// <summary>
        /// The Reviews that have been saved.
        /// </summary>
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Newspaper>()
				.HasAlternateKey(x => x.Email)
				.HasName("Alt_Email");
		}         
    }
}