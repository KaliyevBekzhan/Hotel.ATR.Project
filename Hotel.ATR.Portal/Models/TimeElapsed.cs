using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Hotel.ATR.Portal.Models
{
    public class TimeElapsed : Attribute, IResultFilter
    {
        private Stopwatch stopWatch;
        /*
        private readonly ILogger<TimeElapsed> _logger;
        public TimeElapsed(Logger<TimeElapsed> logger)
        {
            _logger = logger;
        }
        */
        public void OnResultExecuted(ResultExecutedContext context)
        {
            stopWatch.Stop();
            string timeElapsed = stopWatch.ElapsedMilliseconds.ToString();
            //_logger.LogInformation(timeElapsed);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            stopWatch = Stopwatch.StartNew();
        }
    }
}