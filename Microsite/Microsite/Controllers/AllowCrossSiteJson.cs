using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsite.Controllers
{
    public class AllowCrossSiteJson : ActionFilterAttribute
    {
/*        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(actionExecutedContext);
        }*/
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response?.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnResultExecuting(context);
        }
    }
}
