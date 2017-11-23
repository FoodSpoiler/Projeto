import { Endereco } from './fornecedor';

export interface Fornecedor {
  fornecedorId: number;
  nome: string;
  email: string;
  telefone: string;
  celular: string;
  endereco: Endereco;
}

export interface Endereco {
  rua: string;
  bairro: string;
  cep: string;
  cidade: string;
  numero: number;
}

export interface Item {
    itemId: number;
    nome: string;
    descricao: string;
    codigo: string;
    preco: number;
  }
  
  export interface ItemCar {
    itemId: number;
    nome: string;
    descricao: string;
    codigo: string;
    preco: number;
  }

  export interface SavePedido {
    pedidoId: number; 
    nomeUsuario: string;
    emailUsuario: string;
    itens: number[];
  }
  
  export interface Pedido {
    pedidoId: number; 
    nomeUsuario: string;
    emailUsuario: string;
    itens: Item[];
    dataPedido: string;
  }