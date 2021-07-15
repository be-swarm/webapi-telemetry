using MediatR;

using mystrongboxwebapi.core.Models;
using mystrongboxwebapi.core.DBStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using mystrongboxwebapi.Services;

namespace mystrongboxwebapi.telemetry.NotificationHandlers
{



    public class TelemetryApplicationErrorRequestToElasticHandler : INotificationHandler<TelemetryApplicationRequestErrorNotification>, ITelemetryApplicationRequestErrorHandler
    {
        DBElastic elastic;
        ITokenService tokenservice;
        public TelemetryApplicationErrorRequestToElasticHandler(DBElastic _db, ITokenService _tokenservice)
        {
            elastic = _db;
            tokenservice = _tokenservice;
        }
        public async Task Handle(TelemetryApplicationRequestErrorNotification _event, CancellationToken cancellationToken)
        {
            if (Configuration.incommingtelemetry.IsIn(Configuration.incommingtelemetry.applicationerrorrequest, this.GetType().Name))
            {
                await elastic.AddDocument(_event, $"beswarm-applicationrequesterror-{_event.Date.ToString("yyyy.MM.dd")}");
            }
        }
    }
}
