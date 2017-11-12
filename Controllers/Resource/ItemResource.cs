using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Controllers.Resource
{
    public class ItemResource
    {
        public int ItemId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int CardapioId { get; set; }
    }
}

