using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.LedgerMainAccountGroups;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledger main account groups controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class LedgerMainAccountGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="LedgerMainAccountGroupsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public LedgerMainAccountGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new ledger main account group.
        /// </summary>
        /// <param name="ledgerId">The parent ledger identifier.</param>
        /// <param name="model">A ledger main account group model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{ledgerId}/main-account-groups")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.LedgerMainAccountGroup))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int ledgerId, [FromBody] Domain.Models.LedgerMainAccountGroup model)
        {
            await _mediator.Send(new SaveLedgerMainAccountGroupRequest(ledgerId, model));
        }

    }
}