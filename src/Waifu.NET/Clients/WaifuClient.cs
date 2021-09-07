
namespace Waifu
{
	using System;
	using Entities;
	using System.Net;
	using Extensions;
	using System.Net.Http;
	using System.Text.Json;
	using System.Threading;
	using System.Net.Http.Json;
	using System.Threading.Tasks;
	using WaifuShork.Common.QuickLinq;
	using WaifuShork.Common.Exceptions;
	using Microsoft.Extensions.Logging;
	
	public class WaifuClient : IDisposable
	{
		private readonly HttpClient _client;
		private readonly WaifuConfiguration _config;
		private readonly JsonSerializerOptions _options;
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly CancellationToken _cancellationToken;

		public WaifuClient(WaifuConfiguration config)
		{
			_client = new HttpClient();
			_options = new JsonSerializerOptions
			{
				WriteIndented = true,
			};
			
			_cancellationTokenSource = new CancellationTokenSource();
			_cancellationToken = _cancellationTokenSource.Token;
			_config = config;
		}
		
		/// <summary>
		/// Gets 30 SFW images from waifu.net, along with the ability to exclude specific links 
		/// </summary>
		/// <param name="excludes"></param>
		/// <param name="category"></param>
		/// <returns>WaifuImages</returns>
		public async Task<string[]> GetManyRandomSfwAsync(string[] excludes = null, SfwCategory category = SfwCategory.Waifu)
		{
			if (excludes == null)
			{
				excludes = _config.DefaultExcludes;
			}
			else
			{
				excludes.ConcatQ(_config.DefaultExcludes);
			}

			if (_config.Logger != null)
			{
				_config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching 30 Random SFWs");
			}			
			
			var url = Endpoints.GetSfwEndpoint(category, true);
			var response = await _client.PostAsJsonAsync(url, new { exclude = excludes }, _cancellationToken);
			
			if (response == null)
			{
				throw new NeatException("HttpResponseMessage was null, unable to properly return content");
			}
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new NeatException($"Expected 200 HttpStatusCode, instead got {response.StatusCode}");
			}

			var images = await response.DeserializeAsync<WaifuImages>(_options, _cancellationToken);
			return images.Files;
		}

		/// <summary>
		/// Gets 30 NSFW images from waifu.net, along with the ability to exclude specific links 
		/// </summary>
		/// <param name="excludes"></param>
		/// <param name="category"></param>
		/// <returns>WaifuImages</returns>
		public async Task<string[]> GetManyRandomNsfwAsync(string[] excludes = null, NsfwCategory category = NsfwCategory.Waifu)
		{
			if (excludes == null)
			{
				excludes = _config.DefaultExcludes;
			}
			else
			{
				excludes.ConcatQ(_config.DefaultExcludes);
			}

			if (_config.Logger != null)
			{
				_config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching 30 Random NSFWs");
			}
			
			var url = Endpoints.GetNsfwEndpoint(category, true);
			var response = await _client.PostAsJsonAsync(url, new { exclude = excludes }, _cancellationToken);
			
			if (response == null)
			{
				throw new NeatException("HttpResponseMessage was null, unable to properly return content");
			}
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new NeatException($"Expected 200 HttpStatusCode, instead got {response.StatusCode}");
			}
			
			var images = await response.DeserializeAsync<WaifuImages>(_options, _cancellationToken);
			return images.Files;
		}
		
		
		/// <summary>
		/// Gets a single SFW image from waifu.pics 
		/// </summary>
		/// <param name="category"></param>
		/// <returns>WaifuImage</returns>
		public async Task<string> GetRandomSfwAsync(SfwCategory category = SfwCategory.Waifu)
		{
			if (_config.Logger != null)
			{
				_config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching Random SFW");
			}			

			var url = Endpoints.GetSfwEndpoint(category, false);
			var response = await _client.GetAsync(url, _cancellationToken);
			
			if (response == null)
			{
				throw new NeatException("HttpResponseMessage was null, unable to properly return content");
			}
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new NeatException($"Expected 200 HttpStatusCode, instead got {response.StatusCode}");
			}
			
			var image = await response.DeserializeAsync<WaifuImage>(_options, _cancellationToken);
			return image.ImageUrl;
		}

		/// <summary>
		/// Gets a single NSFW image from waifu.pics 
		/// </summary>
		/// <param name="category"></param>
		/// <returns>WaifuImage</returns>
		public async Task<string> GetRandomNsfwAsync(NsfwCategory category = NsfwCategory.Waifu)
		{
			if (_config.Logger != null)
			{
				_config.Logger.Log(LogLevel.Information, LoggerEvents.RestRx, "Fetching Random NSFW");
			}			
			
			var url = Endpoints.GetNsfwEndpoint(category, false);
			var response = await _client.GetAsync(url, _cancellationToken);

			if (response == null)
			{
				throw new NeatException("HttpResponseMessage was null, unable to properly return content");
			}
			
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new NeatException($"Expected 200 HttpStatusCode, instead got {response.StatusCode}");
			}

			var image = await response.DeserializeAsync<WaifuImage>(_options, _cancellationToken);
			return image.ImageUrl;
		}

		public void Dispose()
		{
			if (_client != null)
			{
				_client.Dispose();
			}
			
			GC.SuppressFinalize(this);
		}
	}
}