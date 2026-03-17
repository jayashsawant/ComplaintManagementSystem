using Serilog;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;

        Log.Information("Request: {Method} {Url} {Query} from {IP}",
        request.Method,
        request.Path,
        request.QueryString,
        context.Connection.RemoteIpAddress);

        try
        {
            await _next(context);

            Log.Information("Response Status: {StatusCode}",
                context.Response.StatusCode);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled Exception occurred");
            throw;
        }
    }
}