using System.Net;
using Example.WebApi.Tests.Infrastructure.Models.Base;

namespace Example.WebApi.Tests.Infrastructure.Models;

public class CommonExpectedModel(HttpStatusCode code) : BaseExpectedModel(code)
{
}