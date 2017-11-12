using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Core.models;
using AgilFood.Core.Models;

namespace AgilFood.Controllers.Resource
{
    public class FornecedorResource
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public EnderecoResource Endereco { get; set; }

        public ICollection<ServicoResource> Servicos { get; set; }
        public ICollection<CardapioResource> Cardapios { get; set; }
        public ICollection<Photo> Photos { get; set; }

        
        public FornecedorResource()
        {
            Servicos = new Collection<ServicoResource>();
            Cardapios = new Collection<CardapioResource>();
            Photos = new Collection<Photo>();
        }
    }
}

