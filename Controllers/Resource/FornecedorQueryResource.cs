using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgilFood.Controllers.Resource
{
    public class FornecedorQueryResource
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}

