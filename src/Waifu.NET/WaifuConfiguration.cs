namespace Waifu
{
	using Microsoft.Extensions.Logging;
	
	public sealed class WaifuConfiguration
	{
		public ILogger Logger { internal get; set; }
		public string[] DefaultExcludes { internal get; set; }
	}
}