using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMyDay.Models;
using System.Globalization;
using NLipsum.Core;

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

			//Seed surprise modal
			if (!context.Page.Any())
			{
				SeedSurprise(context);
			}

			//Seed category
			if (!context.Page.Any())
			{
				SeedFaqCategory(context);
			}
		}

		private static void SeedSurprise(HomeMyDayDbContext context)
		{
			var generator = new LipsumGenerator();
			context.Page.Add(new Page() {Page_Name = "TheSurprise", Title = "Surprise", Content = generator.GenerateParagraphs(1)[0] });
			context.SaveChanges();
		}

		private static void SeedFaqCategory(HomeMyDayDbContext context)
		{
			context.FaqCategory.AddRange(new FaqCategory() {CategoryName = "Cat1" }, new FaqCategory() { }, new FaqCategory() { CategoryName = "Cat2" }, new FaqCategory() { CategoryName = "Cat3" });
			context.SaveChanges();
		}

		private static void SeedAccommodations(HomeMyDayDbContext context)
		{
			var generator = new LipsumGenerator();

			context.Accommodations.Add(new Accommodation()
			{
				Price = 310,
				Recommended = true,
				Name = "Casa del Sol",
				Description = String.Join(Environment.NewLine, generator.GenerateParagraphs(2)),
				Continent = "Europe",
				Country = "Spain",
				Location = "Barcelona",
				Beds = 2,
				MaxPersons = 2,
				Rooms = 4,
				CancellationText = generator.GenerateParagraphs(1)[0],
				RulesText = generator.GenerateParagraphs(1)[0],
				PricesText = generator.GenerateParagraphs(1)[0],
				SpaceText = generator.GenerateParagraphs(1)[0],
				ServicesText = generator.GenerateParagraphs(1)[0],
				MediaObjects = new List<MediaObject>()
				{
					new MediaObject()
					{
						Url = "/images/holiday/image-1.jpg",
						Type = MediaType.Image,
						Title = "Indoor living room",
						Description = "Example description",
						Primary = true
					},
					new MediaObject()
					{
						Url = "/images/holiday/image-2.jpg",
						Type = MediaType.Image,
						Title = "Outdoor house",
						Description = "Example description",
					}
				}
			});

			context.Accommodations.Add(new Accommodation()
			{
				Price = 340,
				Recommended = true,
				Name = "Greece House",
				Description = String.Join(Environment.NewLine, generator.GenerateParagraphs(2)),
				Continent = "Europe",
				Country = "Greece",
				Location = "Athens",
				Beds = 1,
				MaxPersons = 2,
				Rooms = 3,
				CancellationText = generator.GenerateParagraphs(1)[0],
				RulesText = generator.GenerateParagraphs(1)[0],
				PricesText = generator.GenerateParagraphs(1)[0],
				SpaceText = generator.GenerateParagraphs(1)[0],
				ServicesText = generator.GenerateParagraphs(1)[0],
				MediaObjects = new List<MediaObject>()
				{
					new MediaObject()
					{
						Url = "/images/holiday/image-3.jpg",
						Type = MediaType.Image,
						Title = "Sea sight",
						Description = "Example description",
						Primary = true
					},
					new MediaObject()
					{
						Url = "/images/holiday/image-4.jpg",
						Type = MediaType.Image,
						Title = "Terras view",
						Description = "Example description"
					}
				}
			});

			context.Accommodations.Add(new Accommodation()
			{
				Price = 320,
				Recommended = false,
				Name = "Germany House",
				Description = String.Join(Environment.NewLine, generator.GenerateParagraphs(2)),
				Continent = "Europe",
				Country = "Germany",
				Location = "Frankfurt",
				Beds = 4,
				MaxPersons = 6,
				Rooms = 7,
				CancellationText = generator.GenerateParagraphs(1)[0],
				RulesText = generator.GenerateParagraphs(1)[0],
				PricesText = generator.GenerateParagraphs(1)[0],
				SpaceText = generator.GenerateParagraphs(1)[0],
				ServicesText = generator.GenerateParagraphs(1)[0],
				MediaObjects = new List<MediaObject>()
				{
					new MediaObject()
					{
						Url = "/images/holiday/image-1.jpg",
						Type = MediaType.Image,
						Title = "Indoor living room",
						Description = "Example description",
						Primary = true
					},
					new MediaObject()
					{
						Url = "/images/holiday/image-2.jpg",
						Type = MediaType.Image,
						Title = "Outdoor house",
						Description = "Example description"
					}
				}
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
				RegionInfo cultureRegion = new RegionInfo(culture.Name);
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
