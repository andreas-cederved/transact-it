using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransactIt.Application.Write.SubAccountGroups;

namespace TransactIt.Api.Controllers
{
    /// <summary>
    /// Ledger sub account groups controller.
    /// </summary>
    [Produces("application/json")]
    [ApiController]
    public class SubAccountGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of <see cref="SubAccountGroupsController"/>.
        /// </summary>
        /// <param name="mediator">The request mediator.</param>
        public SubAccountGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new sub account group.
        /// </summary>
        /// <param name="id">The parent main account group identifier.</param>
        /// <param name="model">A sub account group model sent through request body.</param>
        /// <returns>This is a command it does not send a modeled response.</returns>
        [HttpPost("api/main-account-groups/{id}/sub-account-groups")]
        [SwaggerResponse(200, "Successfully saved data.", typeof(Domain.Models.SubAccountGroup))]
        [SwaggerResponse(400, "Invalid request or data.", typeof(IEnumerable<ValidationFailure>))]
        [SwaggerResponse(404, "Parent entity not found.", typeof(IEnumerable<ValidationFailure>))]
        public async Task Create(
            int id,
            [FromBody] Domain.Models.SubAccountGroup model)
        {
            await _mediator.Send(new SaveSubAccountGroupRequest(id, model));
        }

    }
}