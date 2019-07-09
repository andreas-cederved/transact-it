using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.Accounts;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Accounts controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="AccountsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="id">The parent sub account group identifier.</param>
        /// <param name="model">A account model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/sub-account-groups/{id}/accounts")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.Account))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int id, [FromBody] Domain.Models.Account model)
        {
            await _mediator.Send(new SaveAccountRequest(id, model));
        }

    }
}