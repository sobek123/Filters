using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Filters.Utils
{
    public class CustomPageFilter:IAsyncPageFilter 
    {
        private IPAddress ip;
        private readonly IConfiguration config;

        public CustomPageFilter(IConfiguration _config) { _config = config; }
        
        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            ip = context.HttpContext.Connection.RemoteIpAddress;

            var pageModel = context.HandlerInstance as PageModel ??
                throw new Exception("This page filter must run in a PageModel.");

            pageModel.ViewData["Member"] = ip;
            return next.Invoke();
            //throw new NotImplementedException();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
