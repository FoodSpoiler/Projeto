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
    // quantidade: number;
    nome: string;
    descricao: string;
    codigo: string;
    preco: number;
}

  
  