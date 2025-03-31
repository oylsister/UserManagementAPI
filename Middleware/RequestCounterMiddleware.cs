using UserManagementAPI.Entities;
using UserManagementAPI.Services;

namespace UserManagementAPI.Middleware
{
    public class RequestCounterMiddleware(RequestDelegate next, RequestCounterService service)
    {
        private readonly RequestDelegate _next = next;
        private readonly RequestCounterService _requestCounterService = service;


        public async Task InvokeAsync(HttpContext context)
        {
            // Get the HTTP method of the current request
            string path = context.Request.Path;
            var method = context.Request.Method;

            var data = new RequestLogData
            {
                Uri = path,
                Count = 1
            };

            // Increment the count for the specific method
            _requestCounterService.IncrementCount(method, data);

            // Log the request counts
            var counts = _requestCounterService.GetAllCount();
            Console.WriteLine("Request Counts:");
            foreach (var kvp in counts)
            {
                Console.WriteLine($"{kvp.Key}:");
                foreach (var logData in kvp.Value)
                {
                    Console.WriteLine($"  Uri: {logData.Uri}, Count: {logData.Count}");
                }
            }

            // Call the next middleware
            await _next(context);
        }
    }
}
