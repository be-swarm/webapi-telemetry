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
    public class CreateTelemetryApplicationRequestError : ModelTelemetryApplicationRequestError, IRequest<ResultAction<object>>
    {
    }

    public class CreateTelemetryApplicationRequestErrorHandler : IRequestHandler<CreateTelemetryApplicationRequestError, ResultAction<object>>
    {
        IMediator mediator;
        public CreateTelemetryApplicationRequestErrorHandler(HttpClientService _httpservice, IMediator _mediator)
        {
            mediator = _mediator;
        }

        public async Task<ResultAction<object>> Handle(CreateTelemetryApplicationRequestError request, CancellationToken cancellationToken)
        {


            TelemetryApplicationRequestErrorNotification notif = new();
            notif.From(request);
            // Send notification
            mediator.Publish(notif);
            return new ResultAction<object>();
        }
    }
}
