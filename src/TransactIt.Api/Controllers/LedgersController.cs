using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Read.Ledgers;
using TransactIt.Application.Write.Ledgers;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledgers controller.
    /// </summary>
    [Produces("application/json")]
    [Route("api/ledgers")]
    [ApiController]
    public class LedgersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="LedgersController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public LedgersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all ledgers.
        /// </summary>
        /// <returns>A list of ledgers <see cref="Domain.Models.Ledger"/></returns>
        [HttpGet]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(IEnumerable<Domain.Models.Ledger>))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<IEnumerable<Domain.Models.Ledger>> Get()
        {
            return await _mediator.Send(new FindAllLedgersRequest());
        }

        /// <summary>
        /// Gets the specific ledger by identifier.
        /// </summary>
        /// <param name="id">The ledger identifier.</param>
        /// <returns>A specific ledger <see cref="Domain.Models.Ledger"/></returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(Domain.Models.Ledger))]
        [SwaggerResponse(204, "Request successful, no data found.")]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<Domain.Models.Ledger> Get(int id)
        {
            return await _mediator.Send(new FindLedgerByIdRequest(id));
        }

        /// <summary>
        /// Creates a new ledger.
        /// </summary>
        /// <param name="model">A ledger model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Ledger))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create([FromBody] Domain.Models.Ledger model)
        {
            await _mediator.Send(new SaveLedgerRequest(model));
        }
    }
}