using Microsoft.Extensions.Logging;

namespace Waifu
{
	using Entities;
	using System.Net;
	using System.Text.Json;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Toolkit.Diagnostics;
	
	public class WaifuImageClient
	{
		private readonly WaifuClient _waifuClient;
		internal WaifuImageClient(WaifuClient wf)
		{
			this._waifuClient = wf;
		}
		
		/// <summary>
		/// Gets a single SFW image from waifu.pics 
		/// </summary>
		/// <param name="category"></param>
		/// <returns>WaifuImage</returns>
		public async Task<WaifuImage> GetRandomSfwAsync(SfwCategory category = SfwCategory.Waifu)
		{
			if (this._waifuClient.Config.Logger != null)
			{
				this._waifuClient.Config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching Random SFW");
			}			

			var url = Endpoints.GetSfwEndpoint(category, false);
			var response = await this._waifuClient.HttpClient.GetAsync(url);
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				ThrowHelper.ThrowArgumentException();
			}
			
			await using var stream = await response.Content.ReadAsStreamAsync(CancellationToken.None);
			return await JsonSerializer.DeserializeAsync<WaifuImage>(stream, this._waifuClient.JsonSerializerOptions, this._waifuClient.CancellationToken);
		}

		/// <summary>
		/// Gets a single NSFW image from waifu.pics 
		/// </summary>
		/// <param name="category"></param>
		/// <returns>WaifuImage</returns>
		public async Task<WaifuImage> GetRandomNsfwAsync(NsfwCategory category = NsfwCategory.Waifu)
		{
			if (this._waifuClient.Config.Logger != null)
			{
				this._waifuClient.Config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching Random NSFW");
			}			
			
			var url = Endpoints.GetNsfwEndpoint(category, false);
			var response = await this._waifuClient.HttpClient.GetAsync(url);
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				ThrowHelper.ThrowArgumentException();
			}
			
			await using var stream = await response.Content.ReadAsStreamAsync(CancellationToken.None);
			return await JsonSerializer.DeserializeAsync<WaifuImage>(stream, this._waifuClient.JsonSerializerOptions, this._waifuClient.CancellationToken);
		}
	}
}