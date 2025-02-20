using Domain.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Despesas
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }   
        public DateTime Data { get; set; }
    }
}
