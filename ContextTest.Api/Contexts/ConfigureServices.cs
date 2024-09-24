using ContextTest.Api.Contexts.Accessors;
using ContextTest.Api.Contexts.HTTP;
using ContextTest.Api.Contexts.Providers;

namespace ContextTest.Api.Contexts;

public static class ConfigureServices
{
    private const string CorrelationIdKey = "correlation-id";

    public static IServiceCollection AddContexts(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IContextProvider, ContextProvider>();
        services.AddSingleton<IContextAccessor, ContextAccessor>();
        services.AddTransient<ContextHttpHandler>();

        return services;
    }

    public static IHttpClientBuilder AddContextHandler(this IHttpClientBuilder builder)
        => builder.AddHttpMessageHandler<ContextHttpHandler>();

    public static IApplicationBuilder UseContexts(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            if (!ctx.Request.Headers.TryGetValue(CorrelationIdKey, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString("N");
            }

            ctx.Items.Add(CorrelationIdKey, correlationId);
            return next();
        });

    public static string? GetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var correlationId) ? correlationId?.ToString() : null;
}