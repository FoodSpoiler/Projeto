using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AgilFood.Core;
using AgilFood.Core.models;
using AgilFood.Core.Models;
using AgilFood.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgilFood.Controllers
{
    [Route("/api/fornecedores")]
    public class FornecedoresController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public FornecedoresController(IMapper mapper, IFornecedorRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]  //agora apenas Admins podem criar new vehicles, se caso eu tirasse o Policies.RequireAdminRole, qualquer pessoa logada poderia fazer as operacoes
        public async Task<IActionResult> CreateFornecedor([FromBody] FornecedorResource fornecedorResource)
        {

            var fornecedor = Mapper.Map<FornecedorResource, Fornecedor>(fornecedorResource);

            _repository.Add(fornecedor);
            await _unitOfWork.CompleteAsync();


            return Ok(fornecedor.FornecedorId);
        }

        [HttpGet]
        public async Task<QueryResultResource<FornecedorResource>> GetFornecedores(FornecedorQueryResource filterResource)
        {
            var filter = _mapper.Map<FornecedorQueryResource, FornecedorQuery>(filterResource);

            var queryResult = await _repository.GetFornecedores(filter);

            return _mapper.Map<QueryResult<Fornecedor>, QueryResultResource<FornecedorResource>>(queryResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFornecedor(int id)
        {
            var fornecedor = await _repository.GetFornecedor(id);

            if (fornecedor == null)
                return NotFound();

            var fornecedorResource = _mapper.Map<Fornecedor,FornecedorResource>(fornecedor);

            return Ok(fornecedorResource);
        }

        [HttpPut("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> UpdateFornecedor(int id, [FromBody] FornecedorResource fornecedorResource)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //primeiro vamos achar o fornecedor no banco 
            var fornecedor = await _repository.GetFornecedor(id);

            //Se nao existir esse objeto no banco
            if (fornecedor == null)
            {
                return NotFound();
            }

            Mapper.Map<FornecedorResource, Fornecedor>(fornecedorResource, fornecedor);
            await _unitOfWork.CompleteAsync();

            fornecedor = await _repository.GetFornecedor(fornecedor.FornecedorId);
            var result = _mapper.Map<Fornecedor, FornecedorResource>(fornecedor);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> DeleteFornecedor(int id)
        {
            var fornecedor = await _repository.GetFornecedor(id, includeRelated: false);

            if (fornecedor == null)
                return NotFound();

            _repository.Remove(fornecedor);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}

