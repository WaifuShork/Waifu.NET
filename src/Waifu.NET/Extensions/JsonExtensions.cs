namespace Waifu.Extensions
{
	using System.Net.Http;
	using System.Text.Json;
	using System.Threading;
	using System.Threading.Tasks;

	internal static class JsonExtensions
	{
		internal static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response, JsonSerializerOptions options, CancellationToken token)
		{
			await using var stream = await response.Content.ReadAsStreamAsync(token);
			return await JsonSerializer.DeserializeAsync<T>(stream, options, token);
		}
	}
}