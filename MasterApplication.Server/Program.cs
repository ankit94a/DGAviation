using MasterApplication.DB.Implements;
using MasterApplication.DB.Interface;
using MasterApplication.DB.Services;
using MasterApplication.Server.Authorization;
using MasterApplication.Server.Extensions;
using MasterApplication.Server.Filters;
using MasterApplication.Server.IOC;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// Core services
// ------------------------
IoCConfiguration.Configuration(builder.Services);

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddSingleton<EncriptionService>();
builder.Services.AddSingleton<LoginAttemptService>();
builder.Services.AddSingleton<RSAKeyManager>();

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

// ------------------------
// Memory Cache
// ------------------------
builder.Services.AddMemoryCache();
// ------------------------
// Localization (optional)
// ------------------------
builder.Services.AddLocalization();

// ------------------------
// MVC (Controllers + Filters + SPA support)
// ------------------------
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidateModelFilter>(); // Global model validation
})
.AddDataAnnotationsLocalization() // Support for localized validation messages
.AddJsonOptions(options =>
{
    // Use System.Text.Json
    options.JsonSerializerOptions.DefaultIgnoreCondition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter());
});

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection("JwtConfig")
);
JwtTokenConfig.AddJwtTokenAuthentication(builder.Services, builder.Configuration);

// ------------------------
// Response Compression
// ------------------------
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});


// ------------------------
// Request size limits
// ------------------------
builder.Services.Configure<IISServerOptions>(option =>
{
    option.MaxRequestBodySize = int.MaxValue;
});
builder.Services.Configure<KestrelServerOptions>(option =>
{
    option.Limits.MaxRequestBodySize = int.MaxValue;
});
builder.Services.Configure<FormOptions>(option =>
{
    option.ValueLengthLimit = int.MaxValue;
    option.MultipartBodyLengthLimit = int.MaxValue;
    option.MultipartHeadersLengthLimit = int.MaxValue;
});


var app = builder.Build();

// ------------------------
// Security first
// ------------------------
app.UseHttpsRedirection();
// ------------------------
// Static files (NO auth)
// ------------------------
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapStaticAssets();
// ------------------------
// Compression (early)
// ------------------------
app.UseResponseCompression();

// ------------------------
// Routing
// ------------------------
app.UseRouting();
// ------------------------
// Authentication & Authorization
// ------------------------
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.


app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
