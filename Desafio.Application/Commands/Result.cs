namespace Desafio.Application.Commands
{
    public class Result<T>
    {
        public bool Valido { get; }
        public bool Invalido => !Valido;
        public T? Dados { get; }
        public bool TemDados => Dados is not null;
        public string? Erro { get; }

        private Result(bool valido, string? erro, T? dados)
        {
            Valido = valido;
            Dados = dados;
            Erro = erro;
        }

        public static implicit operator Result<T>(T valor) => new Result<T>(true, null, valor);

        public static Result<T> Sucesso(T dados) => new Result<T>(true, null, dados);
        public static Result<T> Falha(string erro) => new Result<T>(false, erro, default);
    }
}