using HomeMyDay.Core.Services;
using Microsoft.Extensions.Options;

namespace HomeMyDay.Web.Base.Managers.Implementation
{
	public class GoogleApiServiceOptionsManager : IGoogleApiServiceOptionsManager
	{
		private readonly GoogleApiServiceOptions _googleOptions;

		public GoogleApiServiceOptionsManager(IOptions<GoogleApiServiceOptions> googleOpts)
		{
			_googleOptions = googleOpts.Value;
		}

		public string GetClientApiKey()
		{
			return _googleOptions.ClientApiKey;
		}
	}
}
