using Example.WebApi.Entities;
using Example.WebApi.Tests.Infrastructure.Models.Base;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;

public static class UserActModels
{
    public class GetByIdActModel : BaseActModel
    {
        public required Guid UserId { get; init; }
    }

    public class CreateActModel : BaseActModel
    {
        public required UserEntity Dto { get; init; }
    }

    public class UpdateActModel : BaseActModel
    {
        public required UserEntity Dto { get; init; }
    }
}