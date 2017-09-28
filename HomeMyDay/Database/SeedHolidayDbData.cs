using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;

namespace HomeMyDay.Database
{
	public static class SeedHolidayDbData
	{
		public static void Seed(HolidayDbContext context)
		{
			/*
			if(context.Holidays.Any())
			{
				return;
			}

			context.Holidays.Add(new Holiday() {
				Category = "House",
				DepartureDate = new DateTime(2017, 9, 20),
				ReturnDate = new DateTime(2017, 9, 27),
				Price = 310,
				Recommended = false,
				Accommodation = new Accommodation() {
					Name = "Casa del Sol",
					Description = "A nice, sunny house in Spain.",
					Continent = "Europe",
					Country = "Spain",
					Location = "Barcelona",
					Beds = 2,
					MaxPersons = 2,
					Rooms = 4
				}
			});

			context.Holidays.Add(new Holiday() {
				Category = "House",
				DepartureDate = new DateTime(2017, 9, 25),
				ReturnDate = new DateTime(2017, 9, 27),
				Price = 340,
				Recommended = true,
				Accommodation = new Accommodation() {
					Name = "Greece House",
					Description = "A nice, sunny house in Greece.",
					Continent = "Europe",
					Country = "Greece",
					Location = "Athens",
					Beds = 1,
					MaxPersons = 2,
					Rooms = 3
				}
			});

			context.Holidays.Add(new Holiday() {
				Category = "House",
				DepartureDate = new DateTime(2017, 9, 17),
				ReturnDate = new DateTime(2017, 9, 27),
				Price = 320,
				Recommended = false,
				Accommodation = new Accommodation() {
					Name = "Germany House",
					Description = "A nice house in Germany.",
					Continent = "Europe",
					Country = "Germany",
					Location = "Frankfurt",
					Beds = 4,
					MaxPersons = 6,
					Rooms = 7
				}
			});

			context.SaveChanges();
		}*/
		}
	}
}
