using Example.WebApi.Data;

namespace Example.WebApi.Tests.Infrastructure.Models.Base;

public abstract class BaseDataModel
{
    public async Task RunSeedData(IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<EFContext>();

        await InitializeData(context);
    }

    public abstract Task InitializeData(EFContext context);
}