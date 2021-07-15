using MediatR;

using mystrongboxwebapi.core.Models;
using mystrongboxwebapi.core.DBStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{


    public class TelemetryIncommingHttpRequestToElasticHandler: INotificationHandler<TelemetryIncomingHttpRequestNotification>, ITelemetryIncommingHttRequestHandler
    {
        DBElastic elastic;
        public TelemetryIncommingHttpRequestToElasticHandler(DBElastic _db)
        {
            elastic = _db;
        }
        public async Task Handle(TelemetryIncomingHttpRequestNotification _event, CancellationToken cancellationToken)
        {
            if (Configuration.incommingtelemetry.IsIn(Configuration.incommingtelemetry.incomminghttprequest,this.GetType().Name))
            {
                await elastic.AddDocument(_event, $"beswarm-http-{_event.timestamp.ToString("yyyy.MM.dd")}");
            }
        }
    }
}
