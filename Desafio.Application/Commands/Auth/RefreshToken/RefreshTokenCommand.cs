using MediatR;

namespace Desafio.Application.Commands.Auth.RefreshToken
{
    public record RefreshTokenCommand(string Token, string RefreshToken) : IRequest<RefreshTokenCommandResult>;
}
