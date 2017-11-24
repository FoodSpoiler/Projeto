using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.Models;

namespace AgilFood.Persistence
{
    public class PedidoItemRepository
    {
        private readonly AgilFoodDbContext _context;

        public PedidoItemRepository(AgilFoodDbContext context)
        {
            _context = context;
        }

        public void Add(PedidoItem itemPed)
        {
            _context.PedidoItems.Add(itemPed);
        }
    }
}
