using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Core.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public string NomeUsuario { get; set; }  //como o Auth0 pego de la e mando pra ca
        public string EmailUsuario { get; set; } //como o Auth0 pego de la e mando pra ca
        public DateTime DataPedido { get; set; }
        public ICollection<PedidoItem> Itens { get; set; }


        public Pedido()
        {
            Itens = new Collection<PedidoItem>();
        }
    }
}
