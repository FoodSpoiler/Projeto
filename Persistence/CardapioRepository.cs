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


        public void Add(Cardapio cardapio)
        {
            _context.Cardapios.Add(cardapio);
        }

        void ICardapioRepository.Add(Cardapio cardapio)
        {
            throw new NotImplementedException();
        }

        Task<Cardapio> ICardapioRepository.GetCardapio(int id, bool includeRelated)
        {
            throw new NotImplementedException();
        }

        Task<List<Cardapio>> ICardapioRepository.GetCardapios(int id)
        {
            throw new NotImplementedException();
        }

        void ICardapioRepository.Remove(Cardapio cardapio)
        {
            throw new NotImplementedException();
        }
    }
}
