using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Despesas.EditarDespesa.DTO
{
    public class EditarDespesaInput
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public int? Categoria_id { get; set; }
        public DateTime Data { get; set; }
    }
}
