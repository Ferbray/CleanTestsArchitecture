using Example.WebApi.Tests.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Example.WebApi.Tests.Infrastructure.Web.Base;

public abstract class BaseWebApplicationFactory : WebApplicationFactory<Program>
{
    private HttpClient? _httpClient;

    public HttpClient HttpClient
    {
        get
        {
            if (_httpClient != null)
            {
                return _httpClient;
            }

            var httpClient = CreateDefaultClient();
            httpClient.BaseAddress = new Uri(Constants.Configs.BaseAddress);
            _httpClient = httpClient;

            return _httpClient;
        }
    }
}