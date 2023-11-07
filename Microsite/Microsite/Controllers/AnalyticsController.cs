using Microsite.Shared;
using Microsite.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Microsite.Controllers
{
    public class AnalyticsController : Controller
    {
        [HttpGet]
        [Route("api/analytics")]
        public AnalyticsDTO Bonus()
        {
            AnalyticsBusinessContext analytics = new();
            return analytics.Analytic();
        }
    }
}
