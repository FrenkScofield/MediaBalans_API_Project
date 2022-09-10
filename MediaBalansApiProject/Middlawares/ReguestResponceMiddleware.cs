using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Middlawares
{
    public class ReguestResponceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ReguestResponceMiddleware> _logger;

        public ReguestResponceMiddleware(RequestDelegate next, ILogger<ReguestResponceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {

            _logger.LogInformation($"NewQuery:{httpContext.Request.QueryString}");


            await _next.Invoke(httpContext);
        }
    }
}
