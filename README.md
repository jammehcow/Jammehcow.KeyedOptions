# TTools.Configuration.KeyedOptions

`KeyedOptions` is a small library aimed to reduce code duplication (specifically binding with keys) when registering `IOptions` in .NET

## Why?

I've used this chunk of code in a few work projects and found myself needing it in personal ones too.
You might think this isn't worth creating a package for, but here it is!

## Usage

Install the appropriate package for your .NET version:
 - `TTools.Configuration.KeyedOptions.Net6` for projects targeting .NET 6
 - `TTools.Configuration.KeyedOptions.Core3` for projects targeting .NET Core 3.1

Create a class that inherits `IKeyedOptions`:
```c#
class MyCoolOptions : IKeyedOptions {
    public string SectionKey => "MySection";
    
    public string MyValue { get; set; }
}
```

Register your `IKeyedOptions` implementation with the IServiceCollection using the provided extension method:
```c#
// This could be in your ConfigureService method or anywhere an IServiceCollection is present
serviceCollection.AddKeyedOptions<MyCoolOptions>();
```

You can now use `IOptions<MyCoolOptions>` as a constructor parameter to access configuration values.

## License

MIT, see [LICENSE](./LICENSE.txt) for more info.
