using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.EditarDespesa
{
    public interface IEditarDespesa
    {
        public Task<EditarDespesaOutput?> Editar(EditarDespesaInput NovaDespesa);
    }
}
