using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using mystrongboxwebapi.core.Models;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{
    public interface ITelemetryHealthStateHandler
    {
    }


    public class TelemetryHealthStateNotification : INotification
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string prometheus { get; set; }
        public string hostip { get; set; }

        public void From(ModelTelemetryHealthState src)
        {
            id = src.id;
            timestamp = src.timestamp;
            prometheus = src.prometheus;
            hostip = src.hostip;
        }
    }
}
