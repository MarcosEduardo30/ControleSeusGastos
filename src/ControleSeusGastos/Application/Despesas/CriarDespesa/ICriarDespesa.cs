using Application.Despesas.CriarDespesa.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.CriarDespesa
{
    public interface ICriarDespesa
    {
        public Task<CriarDespesaDTO> CriarNovaDespesa(CriarDespesaDTO input);
    }
}
