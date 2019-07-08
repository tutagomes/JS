using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Final.Domain.Entities;
using Final.Service.Services;
using Final.Service.Validators;
using GraphQL;
using Final.Infra.Data.Queries;
using GraphQL.Types;


namespace Final.Application.Controllers
{
    [Route("api/graphql")]
    public class GraphQLController : Controller
    {
        public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema()
            {
                Query = new EatMoreQuery()
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            }).ConfigureAwait(false);

            return Ok(result);
        }
    }
}