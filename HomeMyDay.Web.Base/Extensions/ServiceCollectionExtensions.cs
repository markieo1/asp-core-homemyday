using HomeMyDay.Web.Base.Managers;
using HomeMyDay.Web.Base.Managers.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Web.Base.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the base manager
		/// </summary>
		/// <returns></returns>
		public static IServiceCollection AddWebManagers(this IServiceCollection services)
		{
			services.AddTransient<IAccommodationManager, AccommodationManager>();
			services.AddTransient<IBookingManager, BookingManager>();
			services.AddTransient<ICountryManager, CountryManager>();
			services.AddTransient<IFaqManager, FaqManager>();
			services.AddTransient<INewspaperManager, NewspaperManager>();
			services.AddTransient<IPageManager, PageManager>();
			services.AddTransient<IReviewManager, ReviewManager>();
			services.AddTransient<IVacancyManager, VacancyManager>();

			return services;
		}
	}
}
