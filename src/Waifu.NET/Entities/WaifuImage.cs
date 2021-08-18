namespace Waifu.Entities
{
	using System.Text.Json.Serialization;

	public class WaifuImage
	{
		[JsonPropertyName("url")]
		public string ImageUrl { get; set; }
	}
}