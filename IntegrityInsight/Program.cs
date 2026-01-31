using Dapper;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;
using IntegrityInsight.Domain.ConfigModels;
using IntegrityInsight.Infrastructure.Dapper.TypeHandlers;
using IntegrityInsight.Infrastructure.Implementations.DatabaseProvider;
using IntegrityInsight.Infrastructure.Implementations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IntegrityInsight.Application.Common;

public class Program
{
    public static async Task Main(string[] args)
    {
        // 1. Setup DI
        var services = new ServiceCollection();

        ConfigureServices(services);

        // Logging
        services.AddLogging(cfg => cfg.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "[HH:mm:ss] ";
        }));

        services.AddScoped<IRestService, RestService>();
        services.AddScoped<IJsonService, JsonService>();
        services.AddScoped<IConfigDrivenRestClient, ConfigDrivenRestClient>();
        services.AddScoped<ISqlDataProviderService, SQLiteDataProvider>();

        var provider = services.BuildServiceProvider();

        var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger("Main");

        logger.LogInformation("Application Started..");

        var sqlService = provider.GetRequiredService<ISqlDataProviderService>();
        var restService = provider.GetRequiredService<IConfigDrivenRestClient>();
        var jsonService = provider.GetRequiredService<IJsonService>();

        var t1 = new GetMemberDemographicTestCase(sqlService, restService, jsonService);

        var f1 = await t1.ExecuteTestCase(1);

        logger.LogInformation("Application Completed..");
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json", optional: false)
            .Build();

        // Configuration Bindings
        services.Configure<DbOption>(configuration.GetSection("DbOption"));
        services.Configure<EndpointSettings>(configuration);

        SqlMapper.AddTypeHandler(new SQLiteDateTimeHandler());
        SqlMapper.AddTypeHandler(new SQLiteDateHandler());
    }
}

