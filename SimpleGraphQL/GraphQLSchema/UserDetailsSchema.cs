using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using SimpleGraphQL.GraphQLQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGraphQL.GraphQLSchema
{
    public class UserDetailsSchema:Schema
    {
        public UserDetailsSchema(IServiceProvider serviceProvider):base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<UserDetailsQuery>();
        }
    }
}
