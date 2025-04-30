using Example.WebApi.Tests.Infrastructure.Models;
using Testcontainers.PostgreSql;

namespace Example.WebApi.Tests.Infrastructure.Data;

public sealed class PostgreSqlTestContainer : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    private readonly EFContextConfigs _config = new();

    public EFContextConfigs GetConfig() => _config;

    public PostgreSqlTestContainer()
    {
        _container = new PostgreSqlBuilder()
            .WithDatabase(_config.DbName)
            .WithUsername(_config.UserName)
            .WithPassword(_config.Password)
            .WithPortBinding(_config.Port, true)
            .WithImage("postgres:15")
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        _config.Port = _container.GetMappedPublicPort(_config.Port);
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}