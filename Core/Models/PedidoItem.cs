using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Core.Models
{
    [Table("PedidoItens")]
    public class PedidoItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int PedidoItemId { get; set; }
        //nos nao precisamos de uma propertu ID aqui, pois o ProductId e o OrderId ja incicao um unico item (Seria redundante)
        public int ItemId { get; set; }
        public int PedidoId { get; set; }
        public Item Item { get; set; }
        public Pedido Pedido { get; set; }
    }
}
