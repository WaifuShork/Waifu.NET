namespace Waifu.Entities
{
	using System.Text.Json.Serialization;
	
	internal class WaifuImages
	{
		[JsonPropertyName("files")]
		public string[] Files { get; set; }
	}
}