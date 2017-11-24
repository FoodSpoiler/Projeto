using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AgilFood.Core;
using AgilFood.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Persistence
{
    public class CardapioRepository : ICardapioRepository
    {
        private readonly AgilFoodDbContext _context;

        public CardapioRepository(AgilFoodDbContext context)
        {
            _context = context;
        }


        public async Task<Cardapio> GetCardapio(int id, bool includeRelated = true)
        {
            //depois quando fazer o AJUSTE, fazer pra trazer a LISTA DE CARDAPIOS
            if (!includeRelated)
            {
                return await _context.Cardapios.FindAsync(id);
            }

            return await _context.Cardapios
                              .Include(c => c.Itens) //para trazer os itens por lazyLoad
                            .SingleOrDefaultAsync(c => c.FornecedorId == id);


        }

        public async Task<Cardapio> GetCard(int id, int idFornecedor, bool includeRelated = true)
        {
           
            if (!includeRelated)
            {
                return await _context.Cardapios.FindAsync(id);
            }

            return await _context.Cardapios
                            .SingleOrDefaultAsync(c => c.CardapioId == id && c.FornecedorId == idFornecedor);


        }

        public void Add(Cardapio cardapio)
        {
            _context.Cardapios.Add(cardapio);
        }

        public void Remove(Cardapio cardapio)
        {
            _context.Cardapios.Remove(cardapio);
        }

        public async Task<List<Cardapio>> GetCardapios(int id)
        {
            var cardapios = _context.Cardapios
                                 .Include(c => c.Itens)  //para trazer os itens por Eager Load
                               .Where(c => c.FornecedorId == id);

            return await cardapios.ToListAsync();
        }
    }
}
