using System.Collections.Generic;
using System.Collections.ObjectModel;
using AgilFood.Core.models;

namespace AgilFood.Core.Models
{
    public class Fornecedor
    {
        public int FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string EnderecoRua { get; set; }
        public string EnderecoCidade { get; set; }
        public string EnderecoBairro { get; set; }
        public string EnderecoCEP { get; set; }
        public int EnderecoNumero { get; set; }


        public ICollection<Servico> Servicos { get; set; }
        public ICollection<Cardapio> Cardapios { get; set; }
        public ICollection<Photo> Photos { get; set; }
        

        public Fornecedor()
        {
            Servicos = new Collection<Servico>();
            Cardapios = new Collection<Cardapio>();
            Photos = new Collection<Photo>();
        }
    }
}

