using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core;

namespace AgilFood.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AgilFoodDbContext _context;

        public UnitOfWork(AgilFoodDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
