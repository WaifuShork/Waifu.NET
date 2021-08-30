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
			var images = await this._client.WaifuImages.GetManyRandomNsfwAsync();
			Assert.NotNull(images);
			Assert.NotEmpty(images.Files);

			images = await this._client.WaifuImages.GetManyRandomSfwAsync();
			Assert.NotNull(images);
			Assert.NotEmpty(images.Files);
		}
		
		[Fact]
		public async Task Test_ClientImage()
		{
			var image = await this._client.WaifuImage.GetRandomNsfwAsync();
			Assert.NotNull(image);
			Assert.NotNull(image.ImageUrl);
			
			image = await this._client.WaifuImage.GetRandomSfwAsync();
			Assert.NotNull(image);
			Assert.NotNull(image.ImageUrl);
		}
	}
}