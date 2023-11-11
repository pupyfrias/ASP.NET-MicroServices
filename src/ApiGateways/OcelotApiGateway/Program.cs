using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
builder.Services.AddOcelot().AddCacheManager(settings=> settings.WithDictionaryHandle());

builder.Logging
    .AddConsole()
    .AddDebug();

var app = builder.Build();



app.MapGet("/", () => "Hello World!");
await app.UseOcelot();


app.Run();
