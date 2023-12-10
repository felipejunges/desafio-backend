namespace Desafio.Application.Services.Tokens
{
    public interface IJwtTokenService
    {
        string GerarToken(string identifier, string usuarioId, string nomeUsuario, string role);

        string? ObterIdentifierDoToken(string token);

        string GerarRefreshToken();
    }
}
