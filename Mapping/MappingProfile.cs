using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilFood.Controllers.Resource;
using AgilFood.Core.models;
using AgilFood.Core.Models;
using AutoMapper;

namespace AgilFood.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Fornecedor, FornecedorResource>()
                .ForMember(fr => fr.Endereco, opt => opt.MapFrom(f => new EnderecoResource { Rua = f.EnderecoRua, CEP = f.EnderecoCEP, Cidade = f.EnderecoCidade, Bairro = f.EnderecoBairro, Numero = f.EnderecoNumero}));
            CreateMap<Cardapio, CardapioResource>();
            CreateMap<Item, ItemResource>();
            CreateMap<Servico, ServicoResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Photo, PhotoResource>();
            

            //API Resource to Domain [Para fazer o CRUD]
            CreateMap<FornecedorQueryResource, FornecedorQuery>();
            CreateMap<CardapioResource, Cardapio>();
            CreateMap<FornecedorResource, Fornecedor>()
                .ForMember(f => f.FornecedorId, opt => opt.Ignore())
                .ForMember(f => f.EnderecoRua, opt => opt.MapFrom(fr => fr.Endereco.Rua))
                .ForMember(f => f.EnderecoCEP, opt => opt.MapFrom(fr => fr.Endereco.CEP))
                .ForMember(f => f.EnderecoNumero, opt => opt.MapFrom(fr => fr.Endereco.Numero))
                .ForMember(f => f.EnderecoCidade, opt => opt.MapFrom(fr => fr.Endereco.Cidade))
                .ForMember(f => f.EnderecoBairro, opt => opt.MapFrom(fr => fr.Endereco.Bairro));

            CreateMap<ItemResource, Item>();
            
        }
    }
}

