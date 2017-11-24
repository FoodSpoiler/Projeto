using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core;
using AgilFood.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AgilFoodDbContext _context;

        public PedidoRepository(AgilFoodDbContext context)
        {
            _context = context;
        }


        public async Task<Pedido> GetPedido(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await _context.Pedidos.FindAsync(id);

            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(pi => pi.Item)
                .SingleOrDefaultAsync(p => p.PedidoId == id);

        }

        public async Task<List<Pedido>> GetPedidoPorNome(string email)
        {
            var pedidos = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(pi => pi.Item) //para trazer os itens por Eager Load
                .Where(p => p.EmailUsuario == email);   //Aqui filtro so pelo mes atual


            return await pedidos.ToListAsync();
        }

        public void Add(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public void Remove(Pedido pedido)
        {
            _context.Remove(pedido);
        }

        public async Task<List<Pedido>> GetPedidos()
        {

            var pedidos = _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(pi => pi.Item); //para trazer os itens por Eager Load
            

            return await pedidos.ToListAsync();
        }
    }
}
