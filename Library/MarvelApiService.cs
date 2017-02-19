using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Marvel
{
	public class MarvelApiService : IMarvelApiService
	{
		private const string MarvelDns = "https://gateway.marvel.com/v1/public/";
		private const string MarvelCredentials = "?ts=1&apikey=a81593164289ccb83f5f908ead89b163&hash=f413dc2ff725ee0c871141bf2acc4252";

		public async Task<MarvelApiData<Characters>> GetCharacters(string filter, int limit, int offset)
		{
			var querystring = string.Empty;
			if (!string.IsNullOrEmpty(filter))
				querystring += $"&nameStartsWith={System.Net.WebUtility.UrlEncode(filter)}";
			if (limit > 0)
				querystring += $"&limit={limit.ToString()}";
			if (offset > 0)
				querystring += $"&offset={offset.ToString()}";

			var result = await this.MakeHttpCall<MarvelApiResult<Characters>>("characters", querystring);
			return result.Data;

		}

		public async Task<MarvelApiData<Characters>> GetCharacter(int id)
		{
			var result = await this.MakeHttpCall<MarvelApiResult<Characters>>("characters/" + id, string.Empty);
			return result.Data;
		}

		public async Task<MarvelApiData<Comics>> GetComics(int id)
		{
			var result = await this.MakeHttpCall<MarvelApiResult<Comics>>("characters/" + id + "/comics", string.Empty);
			return result.Data;
		}


		private async Task<TOutput> MakeHttpCall<TOutput>(string query, string filter)
		{

			HttpClient client = new HttpClient();
	
			string url = $"{MarvelDns}{query}{MarvelCredentials}{filter}";


			HttpResponseMessage response = new HttpResponseMessage();

			try
			{

				response = await client.GetAsync(url);

				string responseText = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<TOutput>(responseText);
				}
				else {
					throw new Exception(string.Format("Response Statuscode for {0}: {1}", url, response.StatusCode));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				throw ex;
			}
		}
	}
}
