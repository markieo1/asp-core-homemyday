using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HomeMyDay.Web.Site.Base.Extensions
{
	public static class SessionExtensions
	{
		/// <summary>
		/// Store a complex object in the session, using JSON.
		/// </summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="session">The class to add the method to.</param>
		/// <param name="key">The key where the object should be stored.</param>
		/// <param name="value">The object to be stored.</param>
		public static void Set<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}

		/// <summary>
		/// Retrieve a complex object from the session, using JSON.
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="session">The class to add the method to.</param>
		/// <param name="key">The key of the object.</param>
		/// <returns>The requested object.</returns>
		public static T Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default(T) :
								  JsonConvert.DeserializeObject<T>(value);
		}
	}
}
