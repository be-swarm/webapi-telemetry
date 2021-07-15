
using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mystrongboxwebapi.core;
using mystrongboxwebapi.core.Models;
using mystrongboxwebapi.core.Services.SecureExchange;
using mystrongboxwebapi.telemetry.Commands;

using System.Threading.Tasks;

namespace mystrongboxwebapi.telemetry.Controllers
{
    [ApiVersion("1")]
    [Route("privateapi/v{version:apiVersion}/telemetry")]
    [ApiController]
    [Produces("application/json")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TelemetryController : ControllerBase
    {
        IMediator mediator;
        ISecureExchange secureexchange;
        public TelemetryController(ISecureExchange _secureexchange, IMediator _mediator)
        {
            mediator = _mediator;
            secureexchange = _secureexchange;
        }

        [HttpPost("incommingrequest")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultAction<object>>> IncommingRequest([FromBody] string crypted)
        {
            ResultAction<object> ret = new();
            ResultAction<CreateTelemetryIncommingRequest> unsecured = await secureexchange.UnSecure<CreateTelemetryIncommingRequest>(crypted);
            if (unsecured.IsOk)
            {
                ret = await mediator.Send(unsecured.datas);
            }
            return StatusCode(ret.status, ret);
        }
        [HttpPost("applicationerrorrequest")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultAction<object>>> IncommingApplicationError([FromBody] string crypted)
        {

            ResultAction<object> ret = new();
            ResultAction<CreateTelemetryApplicationRequestError> unsecured = await secureexchange.UnSecure<CreateTelemetryApplicationRequestError>(crypted);
            if (unsecured.IsOk)
            {
                ret = await mediator.Send(unsecured.datas);
            }
            return StatusCode(ret.status, ret);
        }
        [HttpPost("uncatchedexception")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultAction<object>>> UnCatchedException([FromBody] string crypted)
        {
            ResultAction<object> ret = new();
            ResultAction<CreateTelemetryHealthStateUnCatchedException> unsecured = await secureexchange.UnSecure<CreateTelemetryHealthStateUnCatchedException>(crypted);
            if (unsecured.IsOk)
            {
                ret = await mediator.Send(unsecured.datas);
            }
            return StatusCode(ret.status, ret);
        }
        [HttpPost("healthstate")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultAction<object>>> HealthState([FromBody] string crypted)
        {
            ResultAction<object> ret = new();
            ResultAction<CreateTelemetryHealthState> unsecured = await secureexchange.UnSecure<CreateTelemetryHealthState>(crypted);
            if (unsecured.IsOk)
            {
                ret = await mediator.Send(unsecured.datas);
            }
            return StatusCode(ret.status, ret);

        }
    }
}
