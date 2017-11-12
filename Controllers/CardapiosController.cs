using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AgilFood.Core;
using AgilFood.Core.Models;
using AgilFood.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Controllers
{
    [Route("/api/cardapios")]
    public class CardapiosController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICardapioRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CardapiosController(IMapper mapper, ICardapioRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        //[HttpGet]
        //public async Task<IEnumerable<CardapioResource>> GetCardapios()
        //{
        //    var cardapios = await _repository.GetCardapios();
  
        //    return Mapper.Map<List<Cardapio>, List<CardapioResource>>(cardapios);
        //}

        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> CreateCardapio([FromBody] CardapioResource cardapioResource)
        {

            var cardapio = Mapper.Map<CardapioResource, Cardapio>(cardapioResource);
            
            _repository.Add(cardapio);
            await _unitOfWork.CompleteAsync();

            //var result = _mapper.Map<Cardapio, CardapioResource>(cardapio);

            return Ok(cardapio.CardapioId); //estou apenas retornando o Id para facitar a transição de telas na hora do cadastro
            
        }

        

        
    }
}




