using Dapper;
using IntegrityInsight.Application.Contracts.Services;
using IntegrityInsight.Domain.ConfigModels;
using IntegrityInsight.Infrastructure.Dapper.TypeHandlers;
using IntegrityInsight.Infrastructure.Implementations.DatabaseProvider;
using IntegrityInsight.Infrastructure.Implementations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IntegrityInsight.Application.Common;
using IntegrityInsight.Application.Behavior.GetMemberDemographicDetails;
using System.Data.Common;

public class Program
{
    public static async Task Main(string[] args)
    {
        // 1. Setup DI
        var devProvider = BuildProvider("dev.json");

        var qaProvider = BuildProvider("qa.json");

        var logger = devProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Main");

        logger.LogInformation("Application Started..");

        // DEV services
        var devRest = devProvider.GetRequiredService<IDataSource<MemberDemographicResponse>>();
        var qaRest = qaProvider.GetRequiredService<IDataSource<MemberDemographicResponse>>();
        var dataCompare = devProvider.GetRequiredService<IDataComparator>();

        var t1 = new GetMemberDemographicTestCase(devRest, qaRest, dataCompare);

        var ed = await t1.ExecuteTestCaseAsync();

        logger.LogInformation("Application Completed..");
    }

    public static ServiceProvider BuildProvider(string fileName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileName, false)
            .Build();

        var services = new ServiceCollection();

        RegisterServices(services, config);

        return services.BuildServiceProvider();
    }

    public static void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        // Logging
        services.AddLogging(cfg => cfg.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.TimestampFormat = "[HH:mm:ss] ";
        }));

        services.Configure<DbOption>(config.GetSection("DbOption"));
        services.Configure<EndpointSettings>(config);

        services.AddScoped<IRestService, RestService>();
        services.AddScoped<IDataComparator, JsonDiffComparator>();
        services.AddScoped<IConfigDrivenRestClient, ConfigDrivenRestClient>();
        services.AddScoped<ISqlDataProviderService, SQLiteDataProvider>();
        services.AddScoped<IDataSource<MemberDemographicResponse>, MemberApiDataSource>();

        SqlMapper.AddTypeHandler(new SQLiteDateTimeHandler());
        SqlMapper.AddTypeHandler(new SQLiteDateHandler());
    }

    public static void ConfigureServices(string fileName, IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileName, optional: false)
            .Build();
    }
}

