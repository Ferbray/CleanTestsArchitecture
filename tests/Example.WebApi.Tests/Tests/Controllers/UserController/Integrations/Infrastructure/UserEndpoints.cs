using Example.WebApi.Entities;
using Example.WebApi.Tests.Infrastructure.Web.Base;

namespace Example.WebApi.Tests.Tests.Controllers.UserController.Integrations.Infrastructure;

public class UserEndpoints : BaseEndpoints
{
    private const string Url = "/api/users";

    public UserGets Gets = new();
    public UserPosts Posts = new();
    public UserPuts Puts = new();
    public UserDeletes Deletes = new();

    public class UserGets
    {
        public Task<HttpResponseMessage> Get(HttpClient client)
        {
            return client.GetAsync(Url);
        }

        public Task<HttpResponseMessage> GetById(HttpClient client, Guid id)
        {
            return client.GetAsync($"{Url}/{id}");
        }
    }

    public class UserPosts
    {
        public Task<HttpResponseMessage> Create(HttpClient client, UserEntity entity)
        {
            return client.PostAsJsonAsync(Url, entity);
        }
    }

    public class UserPuts
    {
        public Task<HttpResponseMessage> Update(HttpClient client, UserEntity entity)
        {
            return client.PutAsJsonAsync(Url, entity);
        }
    }

    public class UserDeletes
    {
        public Task<HttpResponseMessage> Delete(HttpClient client, Guid id)
        {
            return client.DeleteAsync($"{Url}/{id}");
        }
    }
}