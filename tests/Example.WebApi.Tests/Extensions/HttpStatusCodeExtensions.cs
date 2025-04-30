using System.Net;

namespace Example.WebApi.Tests.Extensions;

public static class HttpStatusCodeExtensions
{
    public static bool IsSuccess(this HttpStatusCode statusCode)
    {
        return (int)statusCode >= 200 && (int)statusCode < 300;
    }
}