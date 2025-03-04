using Application.Services.Usuarios.CriarUsuario.DTO;
using Application.Validacao;

namespace Application.Services.Usuarios.CriarUsuario
{
    internal class CriarUsuarioValidador : IValidador<CriarUsuarioInput>
    {
        public List<Erro> validar(CriarUsuarioInput input)
        {
            List<Erro> erros = new List<Erro>();
            return erros;
        }
    }
}
