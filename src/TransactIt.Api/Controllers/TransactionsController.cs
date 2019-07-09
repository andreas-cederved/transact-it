using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.Transactions;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Transactions controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="TransactionsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <param name="id">The parent ledger identifier.</param>
        /// <param name="model">A transaction model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{id}/transactions")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Transaction))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int id, [FromBody] Domain.Models.Transaction model)
        {
            await _mediator.Send(new SaveTransactionRequest(id, model));
        }

    }
}