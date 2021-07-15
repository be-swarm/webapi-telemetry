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
    public class CreateTelemetryIncommingRequest : ModelTelemetryIncomingHttpRequest, IRequest<ResultAction<object>>
    {
    }

    public class CreateTelemetryIncommingRequestHandler : IRequestHandler<CreateTelemetryIncommingRequest, ResultAction<object>>
    {
        IMediator mediator;
        public CreateTelemetryIncommingRequestHandler(HttpClientService _httpservice, IMediator _mediator)
        {
            mediator = _mediator;
        }

        public async Task<ResultAction<object>> Handle(CreateTelemetryIncommingRequest request, CancellationToken cancellationToken)
        {

           TelemetryIncomingHttpRequestNotification notif = new();
           notif.From(request);
           // Send notification
            mediator.Publish(notif);
            return new ResultAction<object>();
        }
    }
}
