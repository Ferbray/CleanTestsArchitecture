using Example.WebApi.Data;
using Example.WebApi.Tests.Infrastructure.Data;
using Example.WebApi.Tests.Infrastructure.Data.Base;
using Example.WebApi.Tests.Infrastructure.Web.Base;

namespace Example.WebApi.Tests.Infrastructure.Web;

public class CommonWebApplicationFactory : BaseWebApplicationFactory, IAsyncLifetime
{
    private readonly Dictionary<string, string> _config = [];
    private readonly PostgreSqlTestContainer _container = new();

    protected virtual List<BaseDatabaseSeeder> Seeders => [new CommonDatabaseSeeder()];

    public async Task InitializeAsync()
    {
        await _container.InitializeAsync();

        var config = _container.GetConfig();
        _config["ConnectionStrings:Postgres"] = config.ConnectionString;

        await SeedTestDb();
    }


    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(
            (config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json");
                config.AddEnvironmentVariables();
                config.AddInMemoryCollection(_config!);
            }
        );

        return base.CreateHost(builder);
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();

        await base.DisposeAsync();
    }

    private async Task SeedTestDb()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EFContext>();

        foreach (var seeder in Seeders)
        {
            await seeder.Run(context);
        }
    }
}