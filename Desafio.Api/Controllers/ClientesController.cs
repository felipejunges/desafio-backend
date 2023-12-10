using Desafio.Application.Commands.Clientes.AlterarCliente;
using Desafio.Application.Commands.Clientes.ExcluirCliente;
using Desafio.Application.Commands.Clientes.IncluirCliente;
using Desafio.Application.Commands.Clientes.ListarClientes;
using Desafio.Application.Commands.Clientes.ObterCliente;
using Desafio.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> ObterCliente([FromRoute] long id)
        {
            var result = await _mediator.Send(new ObterClienteCommand(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarClientes()
        {
            var result = await _mediator.Send(new ListarClientesCommand());

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> IncluirCliente([FromBody] IncluirClienteCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Created("[controller]/{id}", result.Dados);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> AlterarCliente([FromRoute] long id, [FromBody] AlterarClienteCommand command)
        {
            command.AgregarPropriedades(id);

            var result = await _mediator.Send(command);

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Ok(result.Dados);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> ExcluirCliente([FromRoute] long id)
        {
            var result = await _mediator.Send(new ExcluirClienteCommand(id));

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Ok(result.Dados);
        }
    }
}