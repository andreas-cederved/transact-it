using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactIt.Application.Read.Ledgers;

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
        public async Task<Domain.Models.Ledger> Get(int id)
        {
            return await _mediator.Send(new FindLedgerByIdRequest(id));
        }
    }
}