using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.LedgerAccountGroups;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledger account groups controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class LedgerAccountGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="LedgerAccountGroupsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public LedgerAccountGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new ledger account group.
        /// </summary>
        /// <param name="ledgerId">The parent ledger identifier.</param>
        /// <param name="model">A ledger account group model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{ledgerId}/ledger-account-groups")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.LedgerAccountGroup))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int ledgerId, [FromBody] Domain.Models.LedgerAccountGroup model)
        {
            await _mediator.Send(new SaveLedgerAccountGroupRequest(ledgerId, model));
        }

    }
}