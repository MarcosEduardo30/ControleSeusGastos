using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.CriarDespesa
{
    public record CriarDespesaOutput
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
    }
}
