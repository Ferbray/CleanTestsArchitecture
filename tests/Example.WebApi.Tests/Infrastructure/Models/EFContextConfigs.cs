using Example.WebApi.Tests.Infrastructure.Common;

namespace Example.WebApi.Tests.Infrastructure.Models;

public sealed class EFContextConfigs
{
    public string UserName { get; set; } = "test";
    public string Password { get; set; } = "test_password";
    public string Host { get; set; } = "127.0.0.1";
    public int Port { get; set; } = 5432;
    public string DbName { get; set; } = Constants.Common.Faker.Internet.DomainWord();

    public string ConnectionString =>
        $"Host={Host};" +
        $"Port={Port};" +
        $"Username={UserName};" +
        $"Password={Password};" +
        $"Database={DbName};" +
        "Pooling=true;";
}