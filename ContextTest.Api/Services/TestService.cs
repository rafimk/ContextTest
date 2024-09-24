using ContextTest.Api.Contexts.Accessors;

namespace ContextTest.Api.Services;

public class TestService(IContextAccessor contextAccessor) : ITestService
{
    public string? GetCorrelationId()
    {
        return contextAccessor.Context?.CorrelationId;
    }
}