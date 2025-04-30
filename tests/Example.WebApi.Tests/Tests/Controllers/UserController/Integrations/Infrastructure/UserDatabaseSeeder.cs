using Example.WebApi.Tests.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;

public class UserDatabaseSeeder : BaseDatabaseSeeder
{
    protected override Task InitializeData(DbContext context)
    {
        return Task.CompletedTask;
    }
}