using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;
using System.Globalization;

namespace HomeMyDay.Database
{
	public static class SeedHomeMyDayDbData
	{
		public static void Seed(HomeMyDayDbContext context)
		{
			//Seed accommodations
			if (!context.Accommodations.Any())
			{
				SeedAccommodations(context);				
			}

			//Seed countries
			if (!context.Countries.Any())
			{
				SeedCountries(context);
			}
		}

		private static void SeedAccommodations(HomeMyDayDbContext context)
		{
			context.Accommodations.Add(new Accommodation()
			{
				Price = 310,
				Recommended = false,
				Name = "Casa del Sol",
				Description = "A nice, sunny house in Spain.",
				Continent = "Europe",
				Country = "Spain",
				Location = "Barcelona",
				Beds = 2,
				MaxPersons = 2,
				Rooms = 4
			});

			context.Accommodations.Add(new Accommodation()
			{
				Price = 340,
				Recommended = true,
				Name = "Greece House",
				Description = "A nice, sunny house in Greece.",
				Continent = "Europe",
				Country = "Greece",
				Location = "Athens",
				Beds = 1,
				MaxPersons = 2,
				Rooms = 3
			});

			context.Accommodations.Add(new Accommodation()
			{
				Price = 320,
				Recommended = false,
				Name = "Germany House",
				Description = "A nice house in Germany.",
				Continent = "Europe",
				Country = "Germany",
				Location = "Frankfurt",
				Beds = 4,
				MaxPersons = 6,
				Rooms = 7
			});

			context.SaveChanges();
		}

		private static void SeedCountries(HomeMyDayDbContext context)
		{
			//Generate a list of countries to be deduplicated later.
			var countries = new List<Country>();

			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			foreach (CultureInfo culture in cultures)
			{
				RegionInfo cultureRegion = new RegionInfo(culture.LCID);
				countries.Add(new Country()
				{
					CountryCode = cultureRegion.ThreeLetterISORegionName,
					Name = cultureRegion.EnglishName
				});
			}

			//Deduplicate list
			countries = countries.GroupBy(c => c.CountryCode)
				.Select(i => i.First())
				.ToList();

			context.Countries.AddRange(countries);

			context.SaveChanges();
		}
	}
}
