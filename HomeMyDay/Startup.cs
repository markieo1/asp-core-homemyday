using HomeMyDay.Database;
using HomeMyDay.Models;
using HomeMyDay.Repository;
using HomeMyDay.Repository.Implementation;
using HomeMyDay.Services;
using HomeMyDay.Services.Implementation;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HomeMyDay
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//Add entity framework.
			services.AddDbContext<HomeMyDayDbContext>(options =>
			{

				//This connection string can be changed in appsettings.json.
				options.UseSqlServer(Configuration.GetConnectionString("HomeMyDayConnection"));

			});

			services.AddDbContext<AppIdentityDbContext>(options =>
			{
				//This connection string can be changed in appsettings.json.
				options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
			});

			services.AddIdentity<User, IdentityRole>(config =>
			{
				//Require confirmed email to login
				config.SignIn.RequireConfirmedEmail = true;
			})
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;

				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout               
				options.SlidingExpiration = true;
			});

			//Mail Services setting
			services.Configure<MailServiceOptions>(Configuration.GetSection("SmtpSettings"));

			//Session settings
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
			});

			services.AddTransient<IEmailServices, EmailServices>();
			services.AddTransient<IAccommodationRepository, EFAccommodationRepository>();
			services.AddTransient<ICountryRepository, EFCountryRepository>();
			services.AddTransient<INewspaperRepository, EFNewspaperRepository>();
            services.AddTransient<IVacancieRepository, EFVacancieRepository>();

            services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, HomeMyDayDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler();
			}

			var cultureInfo = new CultureInfo("nl-NL");
			CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseSession();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
            });

			SeedHomeMyDayDbData.Seed(dbContext);
		}
	}
}
