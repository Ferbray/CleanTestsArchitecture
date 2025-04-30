using System.Net;
using Example.WebApi.Entities;
using Example.WebApi.Tests.Helpers;
using Example.WebApi.Tests.Infrastructure.Common;
using Example.WebApi.Tests.Infrastructure.Models;
using Example.WebApi.Tests.Infrastructure.Models.Base;
using Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations;

public partial class UserControllerTests
{
    private static class Models
    {
        public sealed class GetWhenUsersExistsReturnsListOfUsers : BaseTestModel
        {
            #region Fields 

            public readonly ExpectedModel Expected;

            #endregion

            public GetWhenUsersExistsReturnsListOfUsers()
            {
                var ids = Constants.Users.All.Select(x => x.Id).ToList();

                Expected = new ExpectedModel(HttpStatusCode.OK)
                {
                    AssertOperationResult = users =>
                    {
                        Assert.NotNull(users);
                        Assert.NotEmpty(users);
                        Assert.All(users, x => Assert.Contains(x.Id, ids));
                    }
                };
            }

            #region InternalModels 

            public class ExpectedModel(HttpStatusCode code) : BaseExpectedModel<List<UserEntity>>(code)
            {
            }

            #endregion
        }

        public sealed class GetByIdWhenUserExistsReturnsUser : BaseTestModel
        {
            #region Fields 

            public readonly UserActModels.GetByIdActModel Act;
            public readonly ExpectedModel Expected;

            #endregion

            public GetByIdWhenUserExistsReturnsUser()
            {
                var id = Constants.Users.All.First().Id;

                Act = new UserActModels.GetByIdActModel
                {
                    UserId = id
                };

                Expected = new ExpectedModel(HttpStatusCode.OK)
                {
                    AssertOperationResult = user =>
                    {
                        Assert.NotNull(user);
                        Assert.Equal(id, user.Id);
                    }
                };
            }

            #region InternalModels 

            public class ExpectedModel(HttpStatusCode code) : BaseExpectedModel<UserEntity>(code)
            {
            }

            #endregion
        }

        public sealed class CreateWhenUserExistsReturnsBadRequest : BaseTestModel<Data.CommonDataModel>
        {
            #region Fields 

            protected override Data.CommonDataModel DataModel { get; set; }

            public readonly UserActModels.CreateActModel Act;
            public readonly CommonExpectedModel Expected;

            #endregion

            public CreateWhenUserExistsReturnsBadRequest()
            {
                DataModel = new(isExistsFigureUser: true);

                Act = new UserActModels.CreateActModel
                {
                    Dto = DataModel.FigureUser
                };

                Expected = new CommonExpectedModel(HttpStatusCode.BadRequest);
            }
        }

        public sealed class CreateWhenUserExistsShouldNotCreatedUser : BaseTestModel<Data.CommonDataModel>
        {
            #region Fields 

            protected override Data.CommonDataModel DataModel { get; set; }

            public readonly ArrangeModel Arrange;
            public readonly UserActModels.CreateActModel Act;
            public readonly ExpectedModel Expected;

            #endregion

            public CreateWhenUserExistsShouldNotCreatedUser()
            {
                DataModel = new(isExistsFigureUser: true);

                var userExists = DataModel.FigureUser;

                Arrange = new ArrangeModel(HttpStatusCode.OK)
                {
                    UserEmail = userExists.Email,
                    AssertOperationResult = users =>
                    {
                        Assert.NotNull(users);
                        Assert.Single(users);
                    }
                };

                Act = new UserActModels.CreateActModel
                {
                    Dto = userExists
                };

                Expected = new ExpectedModel(HttpStatusCode.BadRequest)
                {
                    UserEmail = userExists.Email,
                    AssertOperationResult = users =>
                    {
                        Assert.NotNull(users);
                        Assert.Single(users);
                    }
                };
            }

            #region InternalModels 

            public class ArrangeModel(HttpStatusCode code) : BaseArrangeModel<List<UserEntity>>(code)
            {
                public required string UserEmail { get; init; }
            }

            public class ExpectedModel(HttpStatusCode code) : BaseExpectedModel<List<UserEntity>>(code)
            {
                public required string UserEmail { get; init; }
            }

            #endregion
        }

        public sealed class CreateWhenUserNotExistsReturnsOk : BaseTestModel<Data.CommonDataModel>
        {
            #region Fields 

            protected override Data.CommonDataModel DataModel { get; set; }

            public readonly UserActModels.CreateActModel Act;
            public readonly CommonExpectedModel Expected;

            #endregion

            public CreateWhenUserNotExistsReturnsOk()
            {
                DataModel = new();

                Act = new UserActModels.CreateActModel
                {
                    Dto = DataModel.FigureUser
                };

                Expected = new CommonExpectedModel(HttpStatusCode.OK);
            }
        }

        public sealed class CreateWhenUserNotExistsShouldCreatedUser : BaseTestModel<Data.CommonDataModel>
        {
            #region Fields 

            protected override Data.CommonDataModel DataModel { get; set; }

            public readonly ArrangeModel Arrange;
            public readonly UserActModels.CreateActModel Act;
            public readonly ExpectedModel Expected;

            #endregion

            public CreateWhenUserNotExistsShouldCreatedUser()
            {
                DataModel = new();

                var userNotExists = DataModel.FigureUser;

                Arrange = new ArrangeModel(HttpStatusCode.OK)
                {
                    UserEmail = userNotExists.Email,
                    AssertOperationResult = Assert.Null
                };

                Act = new UserActModels.CreateActModel
                {
                    Dto = userNotExists
                };

                Expected = new ExpectedModel(HttpStatusCode.OK)
                {
                    UserEmail = userNotExists.Email,
                    AssertOperationResult = user =>
                    {
                        Assert.NotNull(user);
                        Assert.NotEqual(userNotExists.Id, user.Id);
                        Assert.Equal(userNotExists.Email, user.Email);
                        Assert.Equal(userNotExists.Password, user.Password);
                    }
                };
            }

            #region InternalModels 

            public class ArrangeModel(HttpStatusCode code) : BaseArrangeModel<UserEntity>(code)
            {
                public required string UserEmail { get; init; }
            }

            public class ExpectedModel(HttpStatusCode code) : BaseExpectedModel<UserEntity>(code)
            {
                public required string UserEmail { get; init; }
            }

            #endregion
        }

        public sealed class UpdateWhenUserExistsReturnsOk : BaseTestModel<Data.CommonDataModel>
        {
            #region Fields

            protected override Data.CommonDataModel DataModel { get; set; }

            public readonly UserActModels.UpdateActModel Act;
            public readonly CommonExpectedModel Expected;

            #endregion

            public UpdateWhenUserExistsReturnsOk()
            {
                DataModel = new(isExistsFigureUser: true);

                Act = new UserActModels.UpdateActModel
                {
                    Dto = new UserEntity
                    {
                        Id = DataModel.FigureUser.Id,
                        Email = RandomStringGenerator.GenerateEmail(15, true),
                        Password = RandomStringGenerator.Generate(15, true)
                    }
                };

                Expected = new CommonExpectedModel(HttpStatusCode.OK);
            }
        }
    }
}