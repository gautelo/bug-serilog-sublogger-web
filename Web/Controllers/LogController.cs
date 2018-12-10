using System.Web.Http;
using Serilog;

namespace MyOrg.Web.Controllers
{
    [RoutePrefix("api/log")]
    public class LogController : ApiController
    {
        public ILogger Logger { get; } = Log.Logger.ForContext<LogController>();

        [HttpGet, Route]
        public string Get()
        {
            Logger.Information("Getting Log");

            return "Log";
        }
    }
}
