using Common.Logging;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

var authenticationProviderKey = "IdentityApiKey";

// NUGET - Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication()
 .AddJwtBearer(authenticationProviderKey, x =>
 {
     x.Authority = builder.Configuration["IdentityApiClient"]; // IDENTITY SERVER URL
     x.RequireHttpsMetadata = false;
     x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateAudience = false
     };
 });

// Add services to the container.
builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

//builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

builder.Host.UseSerilog(SeriLogger.Configure);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
