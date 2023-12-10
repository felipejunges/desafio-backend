namespace Desafio.Application.Commands.Auth.Login
{
    public record LoginCommandResult(bool Sucesso, string? Erro, string? Token, string? RefreshToken);
}