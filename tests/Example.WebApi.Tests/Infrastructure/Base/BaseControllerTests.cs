
using Example.WebApi.Tests.Infrastructure.Web.Base;

namespace Example.WebApi.Tests.Infrastructure.Base;

public abstract class BaseControllerTests<TFactory, TEndpoints>(TFactory factory, TEndpoints endpoints)
    : IAsyncLifetime
    where TFactory : BaseWebApplicationFactory
    where TEndpoints : BaseEndpoints
{

    protected readonly TFactory Factory = factory;
    protected readonly HttpClient HttpClient = factory.HttpClient;
    protected readonly TEndpoints Endpoints = endpoints;

    public Task InitializeAsync()
    {
        /*
            Это часть кода нужна для того, чтобы перед каждым тестом в коллекции,
            выполнить какой то код.

            Пример: мы авторизуемся все время под админом, значит запишем здесь авторизацию. 
        */

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        /*
            Это часть кода нужна для того, чтобы после каждого теста в коллекции,
            выполнить какой то код. В частности на чистку.

            Пример: мы авторизуемся все время под админом, значит запишем здесь де авторизацию. 
        */

        return Task.CompletedTask;
    }
}