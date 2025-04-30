using System.Net;
using Example.WebApi.Tests.Extensions;

namespace Example.WebApi.Tests.Infrastructure.Models.Base;

public abstract class BaseExpectedModel(HttpStatusCode code)
{
    public bool IsSuccess { get; } = code.IsSuccess();
    public HttpStatusCode Code { get; } = code;

    public void AssertResponse(HttpResponseMessage? response)
    {
        Assert.NotNull(response);
        Assert.Equal(IsSuccess, response.IsSuccessStatusCode);
        Assert.Equal(Code, response.StatusCode);
    }
}

public abstract class BaseExpectedModel<T1>(HttpStatusCode code) : BaseExpectedModel(code)
{
    public required Action<T1?> AssertOperationResult { get; init; }
}

public abstract class BaseExpectedModel<T1, T2>(HttpStatusCode code) : BaseExpectedModel(code)
{
    public required Action<T1?, T2?> AssertOperationResult { get; init; }
}