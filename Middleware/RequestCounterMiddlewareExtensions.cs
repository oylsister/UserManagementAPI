namespace UserManagementAPI.Middleware
{
    public static class RequestCounterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCounter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCounterMiddleware>();
        }
    }
}
