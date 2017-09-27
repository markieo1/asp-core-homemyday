using HomeMyDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace HomeMyDay.Tests
{
    public class SummaryTest
    {
		[Fact]
		public void TestAddedHolidays()
		{
			// Arrange
			Holiday h1 = new Holiday { Id = 1, Description = "Dit is een omschrijving", Price = 123 };
			Holiday h2 = new Holiday { Id = 2, Description = "Dit is een omschrijving", Price = 148 };
			Summary summary = new Summary();

			// Act
			summary.AddHolidayItem(h1);
			summary.AddHolidayItem(h2);

			var expected = 2;
			var actual = summary.Purchases.ToArray();

			// Assert
			Assert.Equal(expected, actual.Length);
		}

		[Fact]
		public void TestRemovedHolidays()
		{
			// Arrange
			Holiday h1 = new Holiday { Id = 1, Description = "Dit is een omschrijving", Price = 123 };
			Holiday h2 = new Holiday { Id = 2, Description = "Dit is een omschrijving", Price = 148 };
			Summary summary = new Summary();

			// Act
			summary.AddHolidayItem(h1);
			summary.AddHolidayItem(h2);
			summary.RemoveHoliday(h1);

			var expected = 0;
			var actual = summary.Purchases;

			// Assert
			Assert.Equal(expected, actual.Where(p => p.Holiday == h1).Count());
		}

		[Fact]
		public void TestClearedSummary()
		{
			// Arrange
			Holiday h1 = new Holiday { Id = 1, Description = "Dit is een omschrijving", Price = 123 };
			Holiday h2 = new Holiday { Id = 2, Description = "Dit is een omschrijving", Price = 148 };
			Summary summary = new Summary();

			// Act
			summary.AddHolidayItem(h1);
			summary.AddHolidayItem(h2);
			summary.ClearSummary();

			var expected = 0;
			var actual = summary.Purchases;

			// Assert
			Assert.Equal(expected, actual.Count());
		}
	}
}
