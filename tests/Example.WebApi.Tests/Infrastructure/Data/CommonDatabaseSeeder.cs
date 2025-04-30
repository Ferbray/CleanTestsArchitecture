using Example.WebApi.Tests.Extensions;
using Example.WebApi.Tests.Infrastructure.Common;
using Example.WebApi.Tests.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Tests.Infrastructure.Data;

public sealed class CommonDatabaseSeeder : BaseDatabaseSeeder
{
    protected override Task InitializeData(DbContext context)
    {
        /*
            Здесь заполним, если нам необходимо заполнять все тесты,
            какими то общими данными.
        */

        return context.InitializeData(Constants.Users.All);
    }
}