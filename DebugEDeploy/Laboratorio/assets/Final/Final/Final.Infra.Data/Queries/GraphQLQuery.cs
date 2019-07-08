using Final.Domain.Entities;
using Final.Domain.Interfaces;
using Final.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphQL;
using Newtonsoft.Json.Linq;

namespace Final.Infra.Data.Queries
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}