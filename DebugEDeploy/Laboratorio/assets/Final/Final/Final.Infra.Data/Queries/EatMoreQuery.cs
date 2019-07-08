using Final.Domain.Entities;
using Final.Domain.GraphQL;
using Final.Domain.Interfaces;
using Final.Infra.Data.Context;
using Final.Infra.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphQL;
using GraphQL.Types;

namespace Final.Infra.Data.Queries
{
    public class EatMoreQuery : ObjectGraphType
    {
        protected SQLiteContext sqliteContext = new SQLiteContext();

        public EatMoreQuery()
        {

            Field<ListGraphType<ProdutoType>>(
                "produtos",
                resolve: context =>
                {
                    var produtos = sqliteContext
                    .Produto;
                    return produtos;
                });

        }
    }
}