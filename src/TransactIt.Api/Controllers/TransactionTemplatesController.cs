using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.TransactionTemplates;
using TransactIt.Application.Read.DistributeAmounts;
using TransactIt.Application.Read.TransactionTemplates;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Transaction templates controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class TransactionTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="TransactionTemplatesController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public TransactionTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a transaction template with rules for how the transaction should be distributed.
        /// </summary>
        /// <param name="id">The parent ledger identifier.</param>
        /// <param name="model">A transaction template model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{id}/transaction-templates")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.TransactionTemplate))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int id, [FromBody] Domain.Models.TransactionTemplate model)
        {
            await _mediator.Send(new SaveTransactionTemplateRequest(id, model));
        }

        /// <summary>
        /// Gets all transaction templates belonging to the specified ledger.
        /// </summary>
        /// <param name="id">The parent ledger identifier.</param>
        /// <returns>An array of transaction templates</returns>
        [HttpGet("api/ledgers/{id}/transaction-templates")]
        [SwaggerResponse(200, "Successfully retrieved data.", typeof(IEnumerable<Domain.Models.TransactionTemplate>))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<IEnumerable<Domain.Models.TransactionTemplate>> Get(int id)
        {
            return await _mediator.Send(new FindAllTransactionTemplatesRequest(id));
        }

        /// <summary>
        /// Get the distribution for the amount using the specified transaction template.
        /// </summary>
        /// <param name="id">The transaction template identifier.</param>
        /// <param name="amount">The amount to distribute</param>
        /// <returns>An array of accounting entries representing the distributed amount.</returns>
        [HttpGet("api/transaction-templates/{id}/distribution")]
        [SwaggerResponse(200, "Successfully distributed amount.", typeof(IEnumerable<Domain.Models.AccountingEntry>))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task<IEnumerable<Domain.Models.AccountingEntry>> Get(int id, [FromQuery] decimal amount)
        {
            return await _mediator.Send(new DistributeAmountRequest(id, amount));
        }

    }
}