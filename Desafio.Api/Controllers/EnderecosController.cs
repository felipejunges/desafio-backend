using Desafio.Application.Commands.Enderecos.AlterarEndereco;
using Desafio.Application.Commands.Enderecos.ExcluirEndereco;
using Desafio.Application.Commands.Enderecos.IncluirEndereco;
using Desafio.Application.Commands.Enderecos.ListarEnderecos;
using Desafio.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [Authorize]
    [Route("api/Clientes/{clienteId}/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnderecosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarEnderecos([FromRoute] long clienteId)
        {
            var result = await _mediator.Send(new ListarEnderecosCommand(clienteId));

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Cliente>>> IncluirEndereco([FromRoute] long clienteId, [FromBody] IncluirEnderecoCommand command)
        {
            command.AgregarPropriedades(clienteId);

            var result = await _mediator.Send(command);

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<long>> AlterarEndereco([FromRoute] long clienteId, [FromRoute] long id, [FromBody] AlterarEnderecoCommand command)
        {
            command.AgregarPropriedades(id, clienteId);

            var result = await _mediator.Send(command);

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Ok(result.Dados);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Endereco>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Cliente>>> ExcluirEndereco([FromRoute] long clienteId, [FromRoute] long id)
        {
            var result = await _mediator.Send(new ExcluirEnderecoCommand(clienteId, id));

            if (result.Invalido)
                return BadRequest(result.Erro);

            return Ok(result);
        }
    }
}