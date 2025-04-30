using System.Net;
using Example.WebApi.Tests.Infrastructure.Models.Base;

namespace Example.WebApi.Tests.Infrastructure.Models;

public class CommonArrangeModel(HttpStatusCode code) : BaseArrangeModel(code)
{
}