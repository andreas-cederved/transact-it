using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.FinancialTransactions;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Financial transactions controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="FinancialTransactionsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public FinancialTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new financial transaction.
        /// </summary>
        /// <param name="ledgerId">The parent ledger identifier.</param>
        /// <param name="model">A financial transaction model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{ledgerId}/financial-transactions")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.FinancialTransaction))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int ledgerId, [FromBody] Domain.Models.FinancialTransaction model)
        {
            await _mediator.Send(new SaveFinancialTransactionRequest(ledgerId, model));
        }

    }
}