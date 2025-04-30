using Example.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Tests.Extensions;

public static class DbContextExtensions
{
    public static async Task InitializeData<TEntity>(
        this DbContext context,
        TEntity seedData,
        CancellationToken cancellationToken = default
    )
        where TEntity : BaseEntity
    {
        var set = context.Set<TEntity>();

        var item = await set.FirstOrDefaultAsync(x => x.Id == seedData.Id, cancellationToken);
        if (item == null)
        {
            set.Add(seedData);
        }
        else
        {
            context.Entry(item).CurrentValues.SetValues(seedData);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public static async Task InitializeData<TEntity>(
        this DbContext context,
        IEnumerable<TEntity> seedData)
        where TEntity : BaseEntity
    {
        var set = context.Set<TEntity>();

        foreach (var i in seedData)
        {
            var item = await set.FirstOrDefaultAsync(x => x.Id == i.Id);
            if (item == null)
            {
                await set.AddAsync(i);
            }
            else
            {
                context.Entry(item).CurrentValues.SetValues(i);
            }
        }

        await context.SaveChangesAsync();
    }
}