using Desafio.Application.Commands.Auth.Login;
using Desafio.Application.Commands.Auth.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginCommandResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LoginCommandResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LoginCommandResult>> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(RefreshTokenCommandResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(RefreshTokenCommandResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RefreshTokenCommandResult>> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);

            if (!response.Sucesso)
                return BadRequest(response);

            return Ok(response);
        }
    }
}