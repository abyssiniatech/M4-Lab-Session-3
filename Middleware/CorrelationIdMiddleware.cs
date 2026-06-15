namespace TmsApi.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        var correlationId =
            Guid.NewGuid().ToString();

        context.Response.Headers
            .Append(
                "X-Correlation-Id",
                correlationId);

        await _next(context);
    }
}