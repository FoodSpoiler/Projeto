using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilFood.Core.Models
{
    [Table("Cardapios")]
    public class Cardapio
    {
        public int CardapioId { get; set; }
        public string Nome { get; set; } 


        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public ICollection<Item> Itens { get; set; }


        public Cardapio()
        {
            Itens = new Collection<Item>();
        }
    }
}
