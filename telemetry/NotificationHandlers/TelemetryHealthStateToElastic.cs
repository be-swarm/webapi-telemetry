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


    public class TelemetryHealthStateToElasticHandler: INotificationHandler<TelemetryHealthStateNotification>, ITelemetryHealthStateHandler
    {
        DBElastic elastic;
        public TelemetryHealthStateToElasticHandler(DBElastic _db)
        {
            elastic = _db;
        }
        public async Task Handle(TelemetryHealthStateNotification _event, CancellationToken cancellationToken)
        {
            if (Configuration.incommingtelemetry.IsIn(Configuration.incommingtelemetry.healthstate,this.GetType().Name))
            {
                await elastic.AddDocument(_event, $"beswarm-healthstate-{_event.timestamp.ToString("yyyy.MM.dd")}");
            }
        }
    }
}
