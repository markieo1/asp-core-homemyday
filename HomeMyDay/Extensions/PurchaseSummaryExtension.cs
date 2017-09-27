using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Extensions
{
    public static class PurchaseSummaryExtension
    {

		public static decimal TotalPrice(this IHolidayRepository repository)
		{
			decimal total = 0;

			foreach (var holiday in repository.Holidays)
			{
				total += holiday?.Price ?? 0;
			}

			return total;
		}
    }
}
