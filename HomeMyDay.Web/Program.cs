using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HomeMyDay.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseKestrel(options =>
			{
				options.Listen(IPAddress.Any, 80);
				options.Listen(IPAddress.Any, 443, listenOptions =>
				{
					var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
					string certLocation = configuration.GetSection("Certificate").GetSection("Mine").GetSection("Location").Value;
					string certPassword = configuration.GetSection("Certificate").GetSection("Mine").GetSection("Password").Value;
					listenOptions.UseHttps(certLocation, certPassword);
				});
			})
			.UseStartup<Startup>()
			.Build();
	}
}
