using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.BuscarDespesa
{
    public interface IBuscarDespesa
    {
        public Task<BuscarDespesaDTO?> Buscar(int id);
    }
}
