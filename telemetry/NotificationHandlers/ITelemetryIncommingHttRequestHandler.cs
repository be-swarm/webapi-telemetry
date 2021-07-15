using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using mystrongboxwebapi.core.Models;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{
    public interface ITelemetryIncommingHttRequestHandler
    {
    }


    public class TelemetryIncomingHttpRequestNotification : INotification
    {
        public string id { get; set; }
        public DateTime timestamp { get; set; }
        public string request { get; set; } = "";
        public string method { get; set; } = "";
        public string clientip { get; set; } = "";
        public string hostip { get; set; } = "";
        public long ellapsed { get; set; } = 0L;
        public int statuscode { get; set; } = 200;

        public void From(ModelTelemetryIncomingHttpRequest src)
        {
            id = src.id;
            timestamp = src.timestamp;
            request = src.request;
            method = src.method;
            clientip = src.clientip;
            hostip = src.hostip;
            ellapsed = src.ellapsed;
            statuscode = src.statuscode;

        }
    }
}
