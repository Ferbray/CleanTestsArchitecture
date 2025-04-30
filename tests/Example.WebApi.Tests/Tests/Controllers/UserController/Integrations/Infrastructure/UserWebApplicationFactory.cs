using Example.WebApi.Tests.Infrastructure.Data;
using Example.WebApi.Tests.Infrastructure.Data.Base;
using Example.WebApi.Tests.Infrastructure.Web;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;

public class UserWebApplicationFactory : CommonWebApplicationFactory
{
    protected override List<BaseDatabaseSeeder> Seeders => [new CommonDatabaseSeeder(), new UserDatabaseSeeder()];
}