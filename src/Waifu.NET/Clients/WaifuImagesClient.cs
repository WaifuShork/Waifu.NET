namespace Waifu
{
	using Entities;
	using System.Net;
	using System.Text.Json;
	using System.Threading;
	using System.Net.Http.Json;
	using System.Threading.Tasks;
	using WaifuShork.Common.QuickLinq;
	using Microsoft.Extensions.Logging;
	using Microsoft.Toolkit.Diagnostics;

	public class WaifuImagesClient
	{
		private readonly WaifuClient _waifuClient;
		internal WaifuImagesClient(WaifuClient config)
		{
			this._waifuClient = config;
		}
		
		/// <summary>
		/// Gets 30 SFW images from waifu.net, along with the ability to exclude specific links 
		/// </summary>
		/// <param name="excludes"></param>
		/// <param name="category"></param>
		/// <returns>WaifuImages</returns>
		public async Task<WaifuImages> GetManyRandomSfwAsync(string[] excludes = null, SfwCategory category = SfwCategory.Waifu)
		{
			if (excludes == null)
			{
				excludes = this._waifuClient.Config.DefaultExcludes;
			}
			else
			{
				excludes.ConcatQ(this._waifuClient.Config.DefaultExcludes);
			}

			if (this._waifuClient.Config.Logger != null)
			{
				this._waifuClient.Config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching 30 Random SFWs");
			}			
			
			var url = Endpoints.GetSfwEndpoint(category, true);
			var response = await this._waifuClient.HttpClient.PostAsJsonAsync(url, new { exclude = excludes }, this._waifuClient.CancellationToken);
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				ThrowHelper.ThrowArgumentException();
			}
			
			await using var stream = await response.Content.ReadAsStreamAsync(CancellationToken.None);
			return await JsonSerializer.DeserializeAsync<WaifuImages>(stream, this._waifuClient.JsonSerializerOptions, this._waifuClient.CancellationToken);
		}

		
		/// <summary>
		/// Gets 30 NSFW images from waifu.net, along with the ability to exclude specific links 
		/// </summary>
		/// <param name="excludes"></param>
		/// <param name="category"></param>
		/// <returns>WaifuImages</returns>
		public async Task<WaifuImages> GetManyRandomNsfwAsync(string[] excludes = null, NsfwCategory category = NsfwCategory.Waifu)
		{
			if (excludes == null)
			{
				excludes = this._waifuClient.Config.DefaultExcludes;
			}
			else
			{
				excludes.ConcatQ(this._waifuClient.Config.DefaultExcludes);
			}

			if (this._waifuClient.Config.Logger != null)
			{
				this._waifuClient.Config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching 30 Random NSFWs");
			}
			var url = Endpoints.GetNsfwEndpoint(category, true);
			var response = await this._waifuClient.HttpClient.PostAsJsonAsync(url, new { exclude = excludes }, this._waifuClient.CancellationToken);
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				ThrowHelper.ThrowArgumentException();
			}
			
			await using var stream = await response.Content.ReadAsStreamAsync(CancellationToken.None);
			return await JsonSerializer.DeserializeAsync<WaifuImages>(stream, this._waifuClient.JsonSerializerOptions, this._waifuClient.CancellationToken);
		}
	}
}