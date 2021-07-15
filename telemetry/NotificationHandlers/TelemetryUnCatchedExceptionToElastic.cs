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


    public class TelemetryUnCatchedExceptionToElasticHandler: INotificationHandler<TelemetryUnCatchedExceptionNotification>, ITelemetryUnCatchedExceptionHandler
    {
        DBElastic elastic;
        public TelemetryUnCatchedExceptionToElasticHandler(DBElastic _db)
        {
            elastic = _db;
        }
        public async Task Handle(TelemetryUnCatchedExceptionNotification _event, CancellationToken cancellationToken)
        {
            if (Configuration.incommingtelemetry.IsIn(Configuration.incommingtelemetry.uncatchedexceptions,this.GetType().Name))
            {
                await elastic.AddDocument(_event, $"beswarm-uncatchedexception-{_event.timestamp.ToString("yyyy.MM.dd")}");
            }
        }
    }
}
