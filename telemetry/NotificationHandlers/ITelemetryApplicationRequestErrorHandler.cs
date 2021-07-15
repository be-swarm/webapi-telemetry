using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using mystrongboxwebapi.core.Models;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{ 
    public interface ITelemetryApplicationRequestErrorHandler
    {
    }
    public class TelemetryApplicationRequestErrorNotification:INotification
    {

        public string Uid { get; set; }
        public DateTime Date { get; set; }
        public int StatusCode { get; set; }
        public string ApplicationId { get; set; }

       public void From (ModelTelemetryApplicationRequestError src)
        {
            Uid = src.Uid;
            Date = src.Date;
            StatusCode = src.StatusCode;
            ApplicationId = src.ApplicationId;
        }
    }
}
