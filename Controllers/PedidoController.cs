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

    [Route("api/Pedidos")]
    public class PedidoController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPedidoRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public PedidoController(IMapper mapper, IPedidoRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<List<PedidoResource>> GetPedidos()
        {
            var pedidos = await repository.GetPedidos();

            return mapper.Map<List<Pedido>, List<PedidoResource>>(pedidos);
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var pedido = await repository.GetPedido(id);

            if (pedido == null)
                return NotFound();

            var pedidoResource = mapper.Map<Pedido, PedidoResource>(pedido);

            return Ok(pedidoResource);
        }

        // GET: api/Pedidos/carlos
        [HttpGet]
        [Route("byemail")]
        public async Task<List<PedidoResource>> GetPedidoPorNome(string email)
        {
            var pedidos = await repository.GetPedidoPorNome(email);

            return mapper.Map<List<Pedido>, List<PedidoResource>>(pedidos);

        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, [FromBody] Pedido pedido)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pro = await repository.GetPedido(id);

            if (pro == null)
                return NotFound();

            //pro = pedido; Automapper ajuda nisso tambem, poderia ter usado o: _context.Entry(pedido).State = EntityState.Modified;
            mapper.Map<Pedido, Pedido>(pedido, pro);
            await unitOfWork.CompleteAsync();

            var result = await repository.GetPedido(pro.PedidoId);

            return Ok(result);
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<IActionResult> PostPedido([FromBody] SavePedidoResource pedidoResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pedido = mapper.Map<SavePedidoResource, Pedido>(pedidoResource);
            pedido.DataPedido = DateTime.Now;

            repository.Add(pedido);
            await unitOfWork.CompleteAsync();

            pedido = await repository.GetPedido(pedido.PedidoId);

            var result = mapper.Map<Pedido, PedidoResource>(pedido);

            return Ok(result);
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pedido = await repository.GetPedido(id);
            if (pedido == null)
            {
                return NotFound();
            }

            repository.Remove(pedido);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}
