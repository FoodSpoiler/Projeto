using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.Models;

namespace AgilFood.Core
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetPedidos();
        Task<Pedido> GetPedido(int id, bool includeRelated = true);
        Task<List<Pedido>> GetPedidoPorNome(string nome); 
        void Add(Pedido pedido);
        void Remove(Pedido pedido);
    }
}
