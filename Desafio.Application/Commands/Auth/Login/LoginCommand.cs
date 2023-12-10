using MediatR;

namespace Desafio.Application.Commands.Auth.Login
{
    public record LoginCommand(string Email, string Senha) : IRequest<LoginCommandResult>;
}