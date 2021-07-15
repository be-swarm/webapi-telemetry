using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using mystrongboxwebapi.core;
using mystrongboxwebapi.telemetry.NotificationHandlers;

using ServiceStack;

namespace mystrongboxwebapi.telemetry
{
    public class IncommingTelemetry
    {
        public List<string> incomminghttprequest { get; set; } = new();
        public List<string> applicationerrorrequest { get; set; } = new();
        public List<string> uncatchedexceptions { get; set; } = new();
        public List<string> healthstate { get; set; } = new();
        public bool IsValid(ILogger logger)
        {
            int a = ValidTelemetryProvider(typeof(ITelemetryIncommingHttRequestHandler), incomminghttprequest, logger);
            int b = ValidTelemetryProvider(typeof(ITelemetryApplicationRequestErrorHandler), applicationerrorrequest, logger);
            int c = ValidTelemetryProvider(typeof(ITelemetryUnCatchedExceptionHandler), uncatchedexceptions, logger);
            int d = ValidTelemetryProvider(typeof(ITelemetryHealthStateHandler), healthstate, logger);
            if ((a + b + c + d) != 0) return false;
            return true;
        }
        protected int ValidTelemetryProvider(Type t, List<string> src, ILogger logger)
        {
            int bad = 0;
            List<string> res = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
           .Where(x => t.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
           .Select(x => x.Name).ToList();
            foreach (var item in src)
            {
                bool ok = false;
                foreach (var item2 in res)
                {
                    if (item.CompareIgnoreCase(item2) == 0)
                    {
                        ok = true;
                        break;
                    }
                }
                if (!ok)
                {
                    bad++;
                    logger.LogError($"{item} is not a valid telemetry provider");
                }
                else
                {
                    logger.LogInformation($"telemetry provider {item} activated");
                }
            }
            return bad;
        }
        public bool IsIn(List<string> src, string me)
        {
            foreach (var item in src)
            {
                if (item.CompareIgnoreCase(me) == 0) return true;
            }
            return false;
        }
    }

   
    public static class Configuration
    {
        public static IncommingTelemetry incommingtelemetry { get; set; }
    }
}
