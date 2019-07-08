using System;
using GraphQL;
using GraphQL.Types;
using Final.Domain.Entities;

namespace Final.Domain.GraphQL
{
    public class ProdutoType : ObjectGraphType<Produto>
    {
        public ProdutoType()
        {
            Name = "Produto";
            
            Field(x => x.Sku).Description("Código do produto");

            Field(x => x.Codigo).Description("Código interno");

            Field(x => x.Name).Description("Nome do produto");

            Field(x => x.PrecoCompra).Description("Preço de Compra");

            Field(x => x.PrecoVenda).Description("Preço de Venda");

        }
    }
}