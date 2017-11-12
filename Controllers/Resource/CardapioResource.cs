using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.Models;

namespace AgilFood.Controllers.Resource
{
    public class CardapioResource
    {
        public int CardapioId { get; set; }
        public string Nome { get; set; }

        public int FornecedorId { get; set; }
        public ICollection<ItemResource> Itens { get; set; }


        public CardapioResource()
        {
            Itens = new Collection<ItemResource>();
        }
    }
}

