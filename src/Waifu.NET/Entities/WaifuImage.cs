namespace Waifu.Entities
{
	using System.Text.Json.Serialization;

	internal class WaifuImage
	{
		[JsonPropertyName("url")]
		public string ImageUrl { get; set; }
	}
}