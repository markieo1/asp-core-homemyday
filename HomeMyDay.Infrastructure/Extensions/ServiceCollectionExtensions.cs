using HomeMyDay.Core.Repository;
using HomeMyDay.Core.Services;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Infrastructure.Identity;
using HomeMyDay.Infrastructure.Repository;
using HomeMyDay.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the infrastructure services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IdentityBuilder identityBuilder)
		{
			//Add entity framework.
			services.AddDbContext<HomeMyDayDbContext>(options =>
			{
				//This connection string can be changed in appsettings.json.
				options.UseSqlServer(configuration.GetConnectionString("HomeMyDayConnection"));

			});

			services.AddDbContext<AppIdentityDbContext>(options =>
			{
				//This connection string can be changed in appsettings.json.
				options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
			});

			identityBuilder.AddEntityFrameworkStores<AppIdentityDbContext>();
			
			services.AddAuthorization(options =>
			{
				options.AddPolicy(IdentityPolicies.Administrator, policy => policy.RequireRole(IdentityRoles.Administrator));
				options.AddPolicy(IdentityPolicies.Booker, policy => policy.RequireRole(IdentityRoles.Booker));
			});

			//Mail Services setting
			services.Configure<MailServiceOptions>(configuration.GetSection("SmtpSettings"));

			//Google API settings
			services.Configure<GoogleApiServiceOptions>(configuration.GetSection("GoogleMapsSettings"));

			services.AddTransient<IEmailServices, EmailServices>();
			services.AddTransient<IAccommodationRepository, EFAccommodationRepository>();
			services.AddTransient<ICountryRepository, EFCountryRepository>();
			services.AddTransient<INewspaperRepository, EFNewspaperRepository>();
			services.AddTransient<IVacancyRepository, EFVacancyRepository>();
			services.AddTransient<IReviewRepository, EFReviewRepository>();
			services.AddTransient<IFaqRepository, EFFaqRepository>();
			services.AddTransient<IPageRepository, EFPageRepository>();

			return services;
		}
	}
}
