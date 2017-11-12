using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AgilFood.Core;
using AgilFood.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgilFood.Controllers
{
    [Route("/api/itens")]
    public class ItensController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ItensController(IMapper mapper, IItemRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> CreateItem([FromBody] ItemResource itemResource)
        {

            var item = Mapper.Map<ItemResource, Item>(itemResource);

            _repository.Add(item);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Item, ItemResource>(item);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<List<ItemResource>> Getitens(int id)
        {
            var itens = await _repository.GetItens(id);

            return Mapper.Map<List<Item>, List<ItemResource>>(itens);
        }

        [HttpGet("{id}/{cardId}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _repository.GetItem(id);

            if (item == null)
                return NotFound();

            var itemResource = _mapper.Map<Item, ItemResource>(item);

            return Ok(itemResource);
        }

        [HttpPut("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemResource itemResource)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //primeiro vamos achar o item no banco 
            var item = await _repository.GetItem(id);

            //Se nao existir esse objeto no banco
            if (item == null)
            {
                return NotFound();
            }

            Mapper.Map<ItemResource, Item>(itemResource, item);
            await _unitOfWork.CompleteAsync();

            item = await _repository.GetItem(item.ItemId);
            var result = _mapper.Map<Item, ItemResource>(item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _repository.GetItem(id, includeRelated: false);

            if (item == null)
                return NotFound();

            _repository.Remove(item);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
        
    }
}

