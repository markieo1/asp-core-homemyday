using HomeMyDay.Core.Models;
using HomeMyDay.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomeMyDay.Infrastructure.Repository
{
	public class MDBAccommodationRepository : IAccommodationRepository
	{
		private string uri = "http://localhost:3000/api/v1/accommodations";

		public IEnumerable<Accommodation> Accommodations => this.GetAccommodations();

		public IEnumerable<Accommodation> GetAccommodations()
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Method = WebRequestMethods.Http.Get;

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);

			var json = reader.ReadToEnd();

			return JsonConvert.DeserializeObject<IEnumerable<Accommodation>>(json);
		}

		public Accommodation GetAccommodation(string id)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + "/" + id);
			request.Method = WebRequestMethods.Http.Get;

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);

			var json = reader.ReadToEnd();

			return JsonConvert.DeserializeObject<Accommodation>(json);
		}

		public IEnumerable<Accommodation> GetRecommendedAccommodations()
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.Method = WebRequestMethods.Http.Get;

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);

			var json = reader.ReadToEnd();

			return JsonConvert.DeserializeObject<IEnumerable<Accommodation>>(json)
				.Where(m => m.Recommended == true);
		}

		public Task<PaginatedList<Accommodation>> List(int page = 1, int pageSize = 10)
		{
			throw new NotImplementedException();
		}

		public Task Save(Accommodation accommodation)
		{
			throw new NotImplementedException();
		}

		public Task Delete(string id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Accommodation> Search(string location, DateTime departure, DateTime returnDate, int amountOfGuests)
		{
			var url = $"{uri}?search={location}&dateFrom={departure.ToString()}&dateTo={returnDate.ToString()}&persons={amountOfGuests}";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = WebRequestMethods.Http.Get;
			
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream dataStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(dataStream);

			var json = reader.ReadToEnd();

			return JsonConvert.DeserializeObject<IEnumerable<Accommodation>>(json);
		}
	}
}
