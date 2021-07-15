using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using mystrongboxwebapi.core.Models;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{
    public interface ITelemetryUnCatchedExceptionHandler
    {
    }


    public class TelemetryUnCatchedExceptionNotification : INotification
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string fromip { get; set; }
        public string exception { get; set; } = "";
        public string httpquery { get; set; } = "";

        public void From(ModelTelemetryUnCatchedException src)
        {
            id = src.id;
            timestamp = src.timestamp;
            fromip = src.fromip;
            exception = src.Exception;
            httpquery = src.HttpQuery;
        }
    }
}
