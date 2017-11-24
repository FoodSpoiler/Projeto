using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core;
using AgilFood.Core.models;
using AgilFood.Core.Models;
using AgilFood.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Persistence
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly AgilFoodDbContext _context;

        public FornecedorRepository(AgilFoodDbContext context)
        {
            _context = context;
        }


        public async Task<Fornecedor> GetFornecedor(int id, bool includeRelated = true)
        {
            return await _context.Fornecedores
                          .SingleOrDefaultAsync(f => f.FornecedorId == id);
        }

        public void Add(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
        }

        public void Remove(Fornecedor fornecedor)
        {
            _context.Fornecedores.Remove(fornecedor);
        }

        public async Task<QueryResult<Fornecedor>> GetFornecedores(FornecedorQuery queryObj)
        {
            var result = new QueryResult<Fornecedor>();

            var query =  _context.Fornecedores
                               //.Include(f => f.Photos)  //para trazer as fotos por Eager Load
                            .AsQueryable();

            //Paging
            result.TotalItems = await query.CountAsync(); //contar quantos elementos temos

            query = query.ApplyPaging(queryObj);

            //return await query.ToListAsync();

            result.Items = await query.ToListAsync();

            return result;

        }
    }
}
