using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepWorkTracker.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DeepWorkTracker.Functions.Functions
{
    public class OutlookResponseFunction
    {
        private readonly ILogger _logger;

        public OutlookResponseFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OutlookResponseFunction>();
        }

        [Function(nameof(OutlookResponseFunction))]
        public void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
        {
            if (!GlobalStates.IsFocusModeEnabled) return;
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
