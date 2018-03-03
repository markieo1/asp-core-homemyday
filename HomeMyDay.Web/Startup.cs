using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc.Razor;
using HomeMyDay.Infrastructure.Database;
using HomeMyDay.Web.Site.Home.Extensions;
using HomeMyDay.Infrastructure.Extensions;
using HomeMyDay.Infrastructure.Identity;
using HomeMyDay.Web.Site.Cms.Extensions;
using HomeMyDay.Web.Base.Extensions;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Halcyon.Web.HAL.Json;
using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Net;

namespace HomeMyDay.Web
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

			services.Configure<RedirectExtension>(Configuration.GetSection("ExternalAddresses"));

			IdentityBuilder identityBuilder = services.AddIdentity<User, IdentityRole>(config =>
			{
				//Require confirmed email to login
				config.SignIn.RequireConfirmedEmail = true;
			}).AddDefaultTokenProviders();

			services.AddInfrastructureServices(Configuration, identityBuilder);
			services.AddWebManagers();

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

			//Session settings
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				options.Cookie.HttpOnly = true;
			});

			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.AreaViewLocationFormats.Clear();

				options.AddHomeViews();
				options.AddCmsViews();
			});

			services.AddMvc(options =>
			{
				options.Filters.Add(new RequireHttpsAttribute());
			})
			.AddJsonOptions(options =>
			{
				options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			})
			.AddMvcOptions(c =>
			{
				var jsonOutputFormatter = new JsonOutputFormatter(JsonSerializerSettingsProvider.CreateSerializerSettings(), ArrayPool<Char>.Shared);
				c.OutputFormatters.Add(new JsonHalOutputFormatter(new string[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }));
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, HomeMyDayDbContext homeMyDayDbContext, AppIdentityDbContext appIdentityDbContext)
		{
			var options = new RewriteOptions();
			options.AddRedirectToHttps();
			app.UseRewriter(options);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
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
					name: "areaRoute",
					template: "{area:exists}/{controller}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			homeMyDayDbContext.Database.Migrate();
			appIdentityDbContext.Database.Migrate();

			SeedHomeMyDayDbData.Seed(homeMyDayDbContext);
			SeedReviewDbData.Seed(homeMyDayDbContext);
			SeedIdentityDbData.Seed(appIdentityDbContext, app.ApplicationServices);
		}
	}
}
