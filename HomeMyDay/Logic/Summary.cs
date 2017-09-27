using HomeMyDay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Models
{
    public class Summary
    {
		private List<Purchase> PurchasesList = new List<Purchase>();

		public virtual IEnumerable<Purchase> Purchases => PurchasesList;

		public virtual void AddHolidayItem(Holiday holiday)
		{
			Purchase purchase = PurchasesList
				.Where(p => p.Holiday.Id == holiday.Id)
				.FirstOrDefault();

			PurchasesList.Add(new Purchase
			{
				Holiday = holiday
			});
		}

		public virtual void RemoveHoliday(Holiday holiday)
		{
			PurchasesList.RemoveAll(p => p.Holiday.Id == holiday.Id);
		}

		public virtual decimal TotalPrice()
		{
			//Purchases.Sum(p => p.Holiday.Price)
			return 0;
		}

		public virtual void ClearSummary()
		{
			PurchasesList.Clear();
		}
    }
}