# Waifu.NET

Unofficial .NET wrapper for [waifu.pics](https://waifu.pics/).

Supports both SFW and NSFW endpoints.

## Setting up Client
```c#
var client = new WaifuClient(new WaifuConfiguration 
{
    // Gives you the option to exclude specific links from queries
    DefaultExcludes = new [] { "exclude links here" },
});
```

## Get Image

```c#
var image = await client.GetRandomSfwAsync(SfwCategory.Waifu);
await Console.Out.WriteLineAsync(image);
```

## Get Images
```c#
var image = await client.GetManyRandomSfwAsync(SfwCategory.Waifu);
foreach (var image in images)
{
    await Console.Out.WriteLineAsync(image);
}
```

## Todo 
- Fixed logging, it works for now but needs to be better.