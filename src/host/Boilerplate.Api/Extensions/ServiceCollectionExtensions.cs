using Boilerplate.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        });
        return host;
    }

    internal static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            })
            .AddPersistence(configuration)
            .AddRouting(options => options.LowercaseUrls = true);
    }

    internal static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config) =>
        builder
            .UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseSwagger()
            .UseSwaggerUI();
}