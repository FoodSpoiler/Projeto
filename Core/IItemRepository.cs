using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.Models;

namespace AgilFood.Core
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItens(int id);
        Task<Item> GetItem(int id, bool includeRelated = true);
        void Add(Item item);
        void Remove(Item item);
    }
}
