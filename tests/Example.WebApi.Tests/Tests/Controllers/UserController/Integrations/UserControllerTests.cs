using Example.WebApi.Data;
using Example.WebApi.Entities;
using Example.WebApi.Tests.Infrastructure.Base;
using Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations;

public partial class UserControllerTests(UserWebApplicationFactory factory, UserEndpoints endpoints)
    : BaseControllerTests<UserWebApplicationFactory, UserEndpoints>(factory, endpoints),
    IClassFixture<UserWebApplicationFactory>,
    IClassFixture<UserEndpoints>
{
    [Fact]
    public async Task Get_WhenUsersExists_ReturnsListOfUsers()
    {
        // Arrange
        var testModel = new Models.GetWhenUsersExistsReturnsListOfUsers();

        // Act
        var response = await Endpoints.Gets.Get(HttpClient);

        // Assert
        testModel.Expected.AssertResponse(response);

        var operationResult = await response.Content.ReadFromJsonAsync<List<UserEntity>>();

        testModel.Expected.AssertOperationResult(operationResult);
    }

    [Fact]
    public async Task GetById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var testModel = new Models.GetByIdWhenUserExistsReturnsUser();

        // Act
        var response = await Endpoints.Gets.GetById(HttpClient, testModel.Act.UserId);

        // Assert
        testModel.Expected.AssertResponse(response);

        var operationResult = await response.Content.ReadFromJsonAsync<UserEntity>();

        testModel.Expected.AssertOperationResult(operationResult);
    }

    [Fact]
    public async Task Create_WhenUserExists_ReturnsBadRequest()
    {
        // Arrange
        var testModel = new Models.CreateWhenUserExistsReturnsBadRequest();
        await testModel.Execute(Factory.Services);

        // Act
        var response = await Endpoints.Posts.Create(HttpClient, testModel.Act.Dto);

        // Assert
        testModel.Expected.AssertResponse(response);
    }

    [Fact]
    public async Task Create_WhenUserExists_ShouldNotCreatedUser()
    {
        // Arrange
        var testModel = new Models.CreateWhenUserExistsShouldNotCreatedUser();
        await testModel.Execute(Factory.Services);

        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<EFContext>();

            var users = await context.Users
                .Where(x => x.Email == testModel.Arrange.UserEmail)
                .ToListAsync();

            testModel.Arrange.AssertOperationResult(users);
        }

        // Act
        await Endpoints.Posts.Create(HttpClient, testModel.Act.Dto);

        // Assert
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<EFContext>();

            var users = await context.Users
                 .Where(x => x.Email == testModel.Expected.UserEmail)
                 .ToListAsync();

            testModel.Expected.AssertOperationResult(users);
        }
    }

    [Fact]
    public async Task Create_WhenUserNotExists_ReturnsOk()
    {
        // Arrange
        var testModel = new Models.CreateWhenUserNotExistsReturnsOk();
        await testModel.Execute(Factory.Services);

        // Act
        var response = await Endpoints.Posts.Create(HttpClient, testModel.Act.Dto);

        // Assert
        testModel.Expected.AssertResponse(response);
    }

    [Fact]
    public async Task Create_WhenUserNotExists_ShouldCreatedUser()
    {
        // Arrange
        var testModel = new Models.CreateWhenUserNotExistsShouldCreatedUser();
        await testModel.Execute(Factory.Services);

        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<EFContext>();

            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Email == testModel.Arrange.UserEmail);

            testModel.Arrange.AssertOperationResult(user);
        }

        // Act
        await Endpoints.Posts.Create(HttpClient, testModel.Act.Dto);

        // Assert
        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<EFContext>();

            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Email == testModel.Expected.UserEmail);

            testModel.Expected.AssertOperationResult(user);
        }
    }

    [Fact]
    public async Task Update_WhenUserExists_ReturnsOk()
    {
        var testModel = new Models.UpdateWhenUserExistsReturnsOk();
        await testModel.Execute(Factory.Services);

        // Act
        var response = await Endpoints.Posts.Create(HttpClient, testModel.Act.Dto);

        // Assert
        testModel.Expected.AssertResponse(response);
    }
}