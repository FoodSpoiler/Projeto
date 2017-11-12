using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgilFood.Migrations
{
    public partial class SeedFornecedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT into Fornecedores (Nome, Celular, Email, EnderecoCEP, EnderecoBairro, EnderecoCidade, EnderecoNumero, EnderecoRua, Telefone) VALUES ('Popedi', '123','email1@gmail','cep1','bairo1','cidade1',1,'rua1','tel1')");
            migrationBuilder.Sql("INSERT into Fornecedores (Nome, Celular, Email, EnderecoCEP, EnderecoBairro, EnderecoCidade, EnderecoNumero, EnderecoRua, Telefone) VALUES ('Chapao','456','email2@gmail','cep2','bairo2','cidade2',2,'rua2','tel2')");
            migrationBuilder.Sql("INSERT into Fornecedores (Nome, Celular, Email, EnderecoCEP, EnderecoBairro, EnderecoCidade, EnderecoNumero, EnderecoRua, Telefone) VALUES ('Kiburgao','789','email3@gmail','cep3','bairo3','cidade3',3,'rua3','tel3')");

            migrationBuilder.Sql("INSERT into Servicos (Nome, Preco, FornecedorId) VALUES ('Entrega', 5.00,(SELECT FornecedorId FROM Fornecedores WHERE Nome= 'Popedi'))");

            migrationBuilder.Sql("INSERT into Cardapios (Nome, FornecedorId) VALUES ('Bebidas', (SELECT FornecedorId FROM Fornecedores WHERE Nome= 'Popedi'))");
            migrationBuilder.Sql("INSERT into Cardapios (Nome, FornecedorId) VALUES ('Marmitas', (SELECT FornecedorId FROM Fornecedores WHERE Nome= 'Popedi'))");

            migrationBuilder.Sql("INSERT into Itens (Nome, Preco, Descricao, CardapioId) VALUES ('Marmita Media', 15.00, 'Tamanho Medio',(SELECT CardapioId FROM Cardapios WHERE Nome= 'Marmitas'))");
            migrationBuilder.Sql("INSERT into Itens (Nome, Preco, Descricao, CardapioId) VALUES ('Marmita Grande', 20.00, 'Tamanho Grande',(SELECT CardapioId FROM Cardapios WHERE Nome= 'Marmitas'))");
            migrationBuilder.Sql("INSERT into Itens (Nome, Preco, Descricao, CardapioId) VALUES ('Coca Lata', 4.00, 'Lata',(SELECT CardapioId FROM Cardapios WHERE Nome= 'Bebidas'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Fornecedores WHERE Nome IN('Popedi', 'Chapao', 'Kiburgao')");
        }
    }
}
