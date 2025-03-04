using Application.Validacao;

namespace Application.Services
{
    public record Resultado<T>(List<Erro>? Erros, T? Valor);
}
