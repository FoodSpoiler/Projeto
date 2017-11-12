using System.ComponentModel.DataAnnotations.Schema;

namespace AgilFood.Core.Models
{
    [Table("Itens")]
    public class Item
    {
        public int ItemId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }

        public int CardapioId { get; set; }
        public Cardapio Cardapio { get; set; }
    }
}

