using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Tests.Infrastructure.Data.Base;

public abstract class BaseDatabaseSeeder
{
    private static readonly Semaphore Semaphore = new(1, 1);

    protected abstract Task InitializeData(DbContext context);

    public async Task Run(DbContext context)
    {
        Semaphore.WaitOne();

        await InitializeData(context);

        Semaphore.Release();
    }
}