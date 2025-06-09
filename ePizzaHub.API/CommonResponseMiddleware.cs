using System.Text.Json;
using System.Threading.Tasks;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ePizzaHub.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CommonResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CommonResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var originalBodyStream=httpContext.Response.Body;
            using(var memoryStream=new MemoryStream())
            {
                httpContext.Response.Body= memoryStream;
                try
                {
                    await _next(httpContext);
                    if (httpContext.Response.ContentType!=null && httpContext.Response.ContentType.Contains("application/json"))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                        var responseObject = new ApiResponseModel<object>(
                             success: httpContext.Response.StatusCode >= 200 && httpContext.Response.StatusCode < 299,
                             data: JsonSerializer.Deserialize<object>(responseBody),
                             message: "request completed successfully");

                        var jsonResponse = JsonSerializer.Serialize(responseObject);
                        httpContext.Response.Body = originalBodyStream;
                        await httpContext.Response.WriteAsync(jsonResponse);

                    }
                }
                catch (Exception ex)
                {
                    httpContext.Response.StatusCode = 500;
                    var errorResponse = new ApiResponseModel<object>(
                        success: false,
                        data: (object)null,
                        message: ex.Message);

                    var jsonResponse = JsonSerializer.Serialize(errorResponse);
                    httpContext.Response.Body = originalBodyStream;
                    await httpContext.Response.WriteAsync(jsonResponse);


                    throw;
                }
            }
            
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CommonResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UseCommonResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CommonResponseMiddleware>();
        }
    }
}
