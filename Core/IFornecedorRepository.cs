using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.models;
using AgilFood.Core.Models;

namespace AgilFood.Core
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor> GetFornecedor(int id, bool includeRelated = true);
        void Add(Fornecedor fornecedor);
        void Remove(Fornecedor fornecedor);
        Task<QueryResult<Fornecedor>> GetFornecedores(FornecedorQuery queryObj);
    }
}
