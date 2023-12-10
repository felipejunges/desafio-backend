using Desafio.Application.Services.Tokens;
using MediatR;

namespace Desafio.Application.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResult>
    {
        private readonly IJwtTokenService _tokenService;

        public RefreshTokenCommandHandler(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var idDoToken = _tokenService.ObterIdentifierDoToken(command.Token);
            if (idDoToken == null)
            {
                return new RefreshTokenCommandResult(false, "Token inválido", null, null);
            }

            // TODO: comparar o refreshToken no usuário ou em uma estrutura de acesso

            throw new NotImplementedException();
        }
    }
}