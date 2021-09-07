using Xunit;
using System.Threading.Tasks;

namespace Waifu.Tests
{
	public class ClientTests
	{
		private readonly WaifuClient _client;
		public ClientTests()
		{
			_client = new WaifuClient(new WaifuConfiguration());
		}
		
		[Fact]
		public async Task Test_ClientImages()
		{
			var images = await _client.GetManyRandomSfwAsync();
			Assert.NotNull(images);
			Assert.NotEmpty(images);

			images = await _client.GetManyRandomSfwAsync();
			Assert.NotNull(images);
			Assert.NotEmpty(images);
		}
		
		[Fact]
		public async Task Test_ClientImage()
		{
			var image = await _client.GetRandomNsfwAsync();
			Assert.NotNull(image);
			Assert.NotNull(image);
			
			image = await _client.GetRandomSfwAsync();
			Assert.NotNull(image);
			Assert.NotNull(image);
		}
	}
}