using HomeMyDay.Models;
using HomeMyDay.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Tests
{
    class FakeHolidayRepository : IHolidayRepository
    {
        public IEnumerable<Holiday> Holidays => new List<Holiday>
        {
            new Holiday {Image = "/images/holiday/image-1.jpg", Description = "Dit is een omschrijving", Recommended = true},
            new Holiday {Image = "/images/holiday/image-2.jpg", Description = "Dit is een omschrijving", Recommended = true},
            new Holiday {Image = "/images/holiday/image-3.jpg", Description = "Dit is een omschrijving", Recommended = true},
            new Holiday {Image = "/images/holiday/image-4.jpg", Description = "Dit is een omschrijving", Recommended = true}
        };

        public IEnumerable<Holiday> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests)
        {
            throw new NotImplementedException();
        }
    }
}
