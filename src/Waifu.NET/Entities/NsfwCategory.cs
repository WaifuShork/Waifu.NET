namespace Waifu.Entities
{
	using WaifuShork.Common.Attributes;

	public enum NsfwCategory
	{
		[Value("waifu")]
		Waifu,
		
		[Value("neko")]
		Neko,
		
		[Value("trap")]
		Trap,
		
		[Value("blowjob")]
		Blowjob
	}
}