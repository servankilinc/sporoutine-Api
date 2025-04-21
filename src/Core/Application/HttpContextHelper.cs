using Microsoft.AspNetCore.Http; 

namespace Application;

public static class HttpContextHelper
{
    public static IHttpContextAccessor? HttpContextAccessor { get; set; }
}
