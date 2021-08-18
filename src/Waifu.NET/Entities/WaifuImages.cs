namespace Waifu.Entities
{
	using System.Text.Json.Serialization;
	
	public class WaifuImages
	{
		[JsonPropertyName("files")]
		public string[] Files { get; set; }
	}
}