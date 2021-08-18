namespace Waifu
{
	using System;
	using System.Net.Http;
	using System.Text.Json;
	using System.Threading;

	public class WaifuClient : IDisposable
	{
		public WaifuClient(WaifuConfiguration config)
		{
			this.HttpClient = new HttpClient();
			this.JsonSerializerOptions = new JsonSerializerOptions
			{
				WriteIndented = true,
			};
			
			this.CancellationTokenSource = new CancellationTokenSource();
			this.Config = config;

			this.WaifuImage = new WaifuImageClient(this);
			this.WaifuImages = new WaifuImagesClient(this);
		}
		
		public WaifuImageClient WaifuImage { get; }
		public WaifuImagesClient WaifuImages { get; }
		
		internal WaifuConfiguration Config { get; }
		internal HttpClient HttpClient { get; set; }
		internal JsonSerializerOptions JsonSerializerOptions { get; set; }
		internal CancellationTokenSource CancellationTokenSource { get; set; }
		internal CancellationToken CancellationToken => CancellationTokenSource.Token;
		
		public void Dispose()
		{
			if (this.HttpClient != null)
			{
				this.HttpClient.Dispose();
			}
			
			GC.SuppressFinalize(this);
		}
	}
}