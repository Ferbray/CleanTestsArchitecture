using Example.WebApi.Data;
using Example.WebApi.Entities;
using Example.WebApi.Tests.Extensions;
using Example.WebApi.Tests.Helpers;
using Example.WebApi.Tests.Infrastructure.Models.Base;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations;

public partial class UserControllerTests
{
    public static class SeedData { }

    private static class Data
    {
        public sealed class CommonDataModel(bool isExistsFigureUser = false) : BaseDataModel
        {
            public readonly UserEntity FigureUser = new()
            {
                Email = RandomStringGenerator.GenerateEmail(15, true),
                Password = RandomStringGenerator.Generate(15, true)
            };

            public override async Task InitializeData(EFContext context)
            {
                if (isExistsFigureUser)
                {
                    await context.InitializeData(FigureUser);
                }
            }
        }
    }
}