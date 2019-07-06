using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.AccountingTemplates;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Accounting templates controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class AccountingTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="AccountingTemplatesController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public AccountingTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a accounting template with rules for how the transaction should be divided.
        /// </summary>
        /// <param name="ledgerId">The parent ledger identifier.</param>
        /// <param name="model">A accounting template model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{ledgerId}/accounting-templates")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.AccountingTemplate))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int ledgerId, [FromBody] Domain.Models.AccountingTemplate model)
        {
            await _mediator.Send(new SaveAccountingTemplateRequest(ledgerId, model));
        }

    }
}