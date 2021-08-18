# Waifu.NET

Unofficial .NET wrapper for [waifu.pics](https://waifu.pics/).

Supports both SFW and NSFW endpoints.

## Setting up Client
```c#
var client = new WaifuClient(new WaifuConfiguration 
{
    DefaultExcludes = new [] { "exclude links here" },
    LoggerFactory = new LoggerFactory().AddSerilog() // Serilog support if you wish
});
```

## Get Image

```c#
var image = await client.WaifuImage.GetRandomSfwAsync(SfwCategory.Waifu);
await Console.Out.WriteLineAsync(image.ImageUrl);
```

## Get Images
```c#
var image = await client.WaifuImages.GetManyRandomSfwAsync(SfwCategory.Waifu);
await Console.Out.WriteLineAsync(image.ImageUrl);
```