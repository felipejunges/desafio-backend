namespace Desafio.Application.Commands.Auth.RefreshToken
{
    public record RefreshTokenCommandResult(bool Sucesso, string? Erro, string? Token, string? RefreshToken);
}