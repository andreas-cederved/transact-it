using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.MainAccountGroups;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Main account groups controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class MainAccountGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="MainAccountGroupsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public MainAccountGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new main account group.
        /// </summary>
        /// <param name="id">The parent ledger identifier.</param>
        /// <param name="model">A ledger main account group model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/ledgers/{id}/main-account-groups")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.MainAccountGroup))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(int id, [FromBody] Domain.Models.MainAccountGroup model)
        {
            await _mediator.Send(new SaveMainAccountGroupRequest(id, model));
        }

    }
}