using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core;
using AgilFood.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Persistence
{
    public class ItemRepository : IItemRepository
    {
        private readonly AgilFoodDbContext _context;
        
        public ItemRepository(AgilFoodDbContext context)
        {
            _context = context;
        }


        public async Task<List<Item>> GetItens(int id)
        {
            var itens = _context.Items
                            .Where(i => i.CardapioId == id);

            return await itens.ToListAsync();
        }

        public async Task<Item> GetItem(int id, bool includeRelated = true)
        {
            if (!includeRelated)
            {
                return await _context.Items.FindAsync(id);
            }

            return await _context.Items
                            .SingleOrDefaultAsync(i => i.ItemId == id);
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
        }

        public void Remove(Item item)
        {
            _context.Items.Remove(item);
        }
    }
}
