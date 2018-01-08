using HomeMyDay.Core.Services;
using HomeMyDay.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMyDay.Infrastructure.Services
{
	public class GoogleMapService : IMapService
	{
		private readonly GoogleApiServiceOptions _options;

		public GoogleMapService(IOptions<GoogleApiServiceOptions> optionsAccessor)
		{
			_options = optionsAccessor.Value;
		}

		public string GetApiKey()
		{
			return _options.ClientApiKey;
		}
	}
}
