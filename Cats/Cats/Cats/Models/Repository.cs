using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
	public class Repository
	{
		public async Task<List<Cat>> GetCats()
		{
			List<Cat> Cats;
			var URLWebAPI = "http://demos.ticapacitacion.com/cats";
			using (var Client = new HttpClient())
			{
				var JSON = await Client.GetStringAsync(URLWebAPI);
				Cats = JsonConvert.DeserializeObject<List<Cat>>(JSON);
			}
			return Cats;
		}
	}
}
