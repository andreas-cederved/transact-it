using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.LedgerSubAccountGroups;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledger sub account groups controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class LedgerSubAccountGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="LedgerSubAccountGroupsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public LedgerSubAccountGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new ledger sub account group.
        /// </summary>
        /// <param name="ledgerId">The grand parent ledger identifier.</param>
        /// <param name="ledgerMainAccountGroupId">The parent ledger main account group identifier.</param>
        /// <param name="model">A ledger sub account group model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{ledgerId}/main-account-groups/{ledgerMainAccountGroupId}/sub-account-groups")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.LedgerSubAccountGroup))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(
            int ledgerId,
            int ledgerMainAccountGroupId,
            [FromBody] Domain.Models.LedgerSubAccountGroup model)
        {
            await _mediator.Send(new SaveLedgerSubAccountGroupRequest(ledgerId, ledgerMainAccountGroupId, model));
        }

    }
}