using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Despesas.BuscarDespesa.DTO
{
    public record BuscarDespesaOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
        public string? Categoria_Nome { get; set; }

        public string Usuario_Nome { get; set; }
    }
}
