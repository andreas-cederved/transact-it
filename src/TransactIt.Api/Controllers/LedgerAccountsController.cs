using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.LedgerAccounts;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledger accounts controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class LedgerAccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="LedgerAccountsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public LedgerAccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new ledger account.
        /// </summary>
        /// <param name="ledgerSubAccountGroupId">The parent ledger sub account group identifier.</param>
        /// <param name="model">A ledger account model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/sub-account-groups/{ledgerSubAccountGroupId}/accounts")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.LedgerAccount))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int ledgerSubAccountGroupId, [FromBody] Domain.Models.LedgerAccount model)
        {
            await _mediator.Send(new SaveLedgerAccountRequest(ledgerSubAccountGroupId, model));
        }

    }
}