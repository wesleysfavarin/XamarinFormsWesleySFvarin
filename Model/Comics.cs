using System;
using Newtonsoft.Json;

namespace Marvel
{
	public class Comics
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
	}
}
