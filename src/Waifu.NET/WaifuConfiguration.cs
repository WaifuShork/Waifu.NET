namespace Waifu
{
	using Microsoft.Extensions.Logging;
	
	public sealed class WaifuConfiguration
	{
		public ILoggerFactory LoggerFactory { internal get; set; }
		public ILogger Logger { internal get; set; }
		public string[] DefaultExcludes { internal get; set; }
	}
}