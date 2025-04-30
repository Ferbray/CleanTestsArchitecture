using Bogus;
using Example.WebApi.Entities;

namespace Example.WebApi.Tests.Infrastructure.Common;

public class Constants
{
    public static class Configs
    {
        public const string BaseAddress = "https://localhost:5000";
    }

    public static class Common
    {
        public static readonly Faker Faker = new();
    }

    public static class Users
    {
        public static readonly UserEntity User1 = new()
        {
            Email = Common.Faker.Internet.Email(),
            Password = Common.Faker.Internet.Password()
        };

        public static readonly UserEntity User2 = new()
        {
            Email = Common.Faker.Internet.Email(),
            Password = Common.Faker.Internet.Password()
        };

        public static readonly UserEntity User3 = new()
        {
            Email = Common.Faker.Internet.Email(),
            Password = Common.Faker.Internet.Password()
        };

        public static List<UserEntity> All => [User1, User2, User3];
    }
}