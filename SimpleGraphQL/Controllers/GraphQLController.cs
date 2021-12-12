using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleGraphQL.Models;
using System.Threading.Tasks;
using GraphQL.NewtonsoftJson;
using System;


/*
    1, Install-Package GraphQL 
    2, Install-Package GraphQL.NewtonsoftJson 
    3, Install GraphQL.Server.Core  for services.AddGraphQL();
    4, Install GraphQL.Server.Transports.AspNetCore.NewtonsoftJson for services.AddGraphQL().AddNewtonsoftJson();
    5, Install GraphiQL for app.UseGraphiQl();
*/




namespace SimpleGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {

        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ILogger<GraphQLController> _logger;
        public GraphQLController(ISchema schema, IDocumentExecuter documentExecuter
            , ILogger<GraphQLController> logger)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {

            var result = await _documentExecuter.ExecuteAsync(
                _ =>
                {
                    _.Schema = _schema;
                    _.Query = query.Query;
                    _.Inputs = query.Variables?.ToInputs();
                });
            var response = new DocumentWriter(indent: true).WriteToStringAsync(result);
            if (result.Errors==null)
            {
                return Ok(result);
            }
            else
            {
                _logger.LogError($"{string.Join(Environment.NewLine, result.Errors)}");
                throw new Exception("Error while executing graphlq query");
            }
        }

    }

}


/*
issue :  System.InvalidOperationException: Synchronous operations are disallowed. Call ReadAsync or set AllowSynchronousIO to true instead.
         GraphQL.Server.Transports.AspNetCore.NewtonsoftJson.GraphQLRequestDeserializer.DeserializeFromJsonBodyAsync(HttpRequest httpRequest, CancellationToken cancellationToken) in
Fix   :   add the below code in startup 
            public void ConfigureServices(IServiceCollection services)
            {
                // If using Kestrel:
                services.Configure<KestrelServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                });

                // If using IIS:
                services.Configure<IISServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                });
            }
*/

/*  
  issue : System.InvalidOperationException: Unable to resolve service for type 'GraphQL.Types.ISchema' while attempting to activate 'GraphQL.Server.DefaultGraphQLExecuter`1[GraphQL.Types.ISchema]'.
  Fix   :services.AddSingleton<ISchema, UserDetailsSchema>();
 */


/* 
 Issue  : No service for type 'SimpleGraphQL.GraphQLQueries.UserDetailsQuery' has been registered.
 Fix    :  one if this was missing the not oeprator (!)  (!t.IsAbstract && !t.IsInterface)
 */

/* 
 Issue : Required service for type GraphQL.Types.AutoRegisteringInputObjectGraphType`1[SimpleGraphQL.GraphQLTypes.UserDetailsInputType] not found
  Fix  :    Changed the QueryArgument  type from  
            QueryArgument<AutoRegisteringInputObjectGraphType<UserDetailsInputType>>
            to 
            QueryArgument<NonNullGraphType<UserDetailsInputType>>
 */
