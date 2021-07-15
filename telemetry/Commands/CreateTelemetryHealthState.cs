using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using mystrongboxwebapi.core;
using mystrongboxwebapi.core.Models;
using mystrongboxwebapi.core.NotificationHandlers;
using mystrongboxwebapi.core.Services.SecureExchange;
using mystrongboxwebapi.telemetry.NotificationHandlers;

using Newtonsoft.Json;

namespace mystrongboxwebapi.telemetry.Commands
{
    public class CreateTelemetryHealthState : ModelTelemetryHealthState, IRequest<ResultAction<object>>
    {

    }

    public class CreateTelemetryHealthStateHandler : IRequestHandler<CreateTelemetryHealthState, ResultAction<object>>
    {

        IMediator mediator;
        public CreateTelemetryHealthStateHandler(HttpClientService _httpservice, IMediator _mediator)
        {

            mediator = _mediator;
        }

        public async Task<ResultAction<object>> Handle(CreateTelemetryHealthState request, CancellationToken cancellationToken)
        {

            TelemetryHealthStateNotification notif = new();
            notif.From(request);
            // Send notification
            mediator.Publish(notif);
            return new ResultAction<object>();
        }
    }
}
