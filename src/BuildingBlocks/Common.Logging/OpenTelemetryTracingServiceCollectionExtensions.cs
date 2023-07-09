using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace Common.Logging
{
    public static class OpenTelemetryTracingServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenTelemetryTracing(this IServiceCollection services, IConfiguration configuration)
        {
            var activitySource = new ActivitySource(configuration["OpenTelemetry:ServiceName"]);
            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                    tracerProviderBuilder
                        .AddSource(activitySource.Name)
                        .ConfigureResource(resource => resource
                            .AddService(configuration["OpenTelemetry:ServiceName"]))
                        .AddAspNetCoreInstrumentation()
                        .AddZipkinExporter(zipkinOptions =>
                        {
                            zipkinOptions.Endpoint = new Uri(configuration["Zipkin:Endpoint"]);
                        }))
                .WithMetrics(metricsProviderBuilder =>
                    metricsProviderBuilder
                        .ConfigureResource(resource => resource
                            .AddService(configuration["OpenTelemetry:ServiceName"]))
                        .AddAspNetCoreInstrumentation());
                        //.AddConsoleExporter());


            return services;
        }
        public static IServiceCollection AddOpenTelemetryTracingHttp(this IServiceCollection services, IConfiguration configuration)
        {
            var activitySource = new ActivitySource(configuration["OpenTelemetry:ServiceName"]);
            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                    tracerProviderBuilder
                        .AddSource(activitySource.Name)
                        .ConfigureResource(resource => resource
                            .AddService(configuration["OpenTelemetry:ServiceName"]))
                        .AddHttpClientInstrumentation()
                        .AddZipkinExporter(zipkinOptions =>
                        {
                            zipkinOptions.Endpoint = new Uri(configuration["Zipkin:Endpoint"]);
                        }))
                .WithMetrics(metricsProviderBuilder =>
                    metricsProviderBuilder
                        .ConfigureResource(resource => resource
                            .AddService(configuration["OpenTelemetry:ServiceName"]))
                        .AddHttpClientInstrumentation());
                        //.AddConsoleExporter());


            return services;
        }
    }
}
