using Desafio.Application.Services.Tokens;
using Desafio.Domain.Repositories;
using MediatR;

namespace Desafio.Application.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _tokenService;

        public LoginCommandHandler(IUsuarioRepository usuarioRepository, IJwtTokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterUsuarioAtivoPeloEmail(command.Email);

            if (usuario == null || !usuario.Senha.Check(command.Senha))
            {
                return new LoginCommandResult(false, "E-mail e/ou senha inválido(s)", null, null);
            }

            var token = _tokenService.GerarToken(
                Guid.NewGuid().ToString(),
                usuario.Id.ToString(),
                usuario.Nome,
                usuario.Role);

            var refreshToken = _tokenService.GerarRefreshToken();

            // TODO: armazenar o refreshToken no usuário ou em uma estrutura de acesso

            return new LoginCommandResult(true, null, token, refreshToken);
        }
    }
}