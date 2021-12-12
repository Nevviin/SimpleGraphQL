using GraphQL;
using GraphQL.Types;
using SimpleGraphQL.GraphQLTypes;
using SimpleGraphQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleGraphQL.GraphQLQueries
{
    public class UserDetailsQuery:ObjectGraphType
    {
        public UserDetailsQuery()
        {
            Field<UserDetailsOutPutType>(
                name: "getUserDetails"
                , arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserDetailsInputType>>
                    { Name ="userDetailsInput"}
                ),
                resolve: context =>
                {
                    var requestInput = context.GetArgument<UserDetailsInputModel>("userDetailsInput");
                    return new UserDetailsOutPutModel() { City = "Sydney", Id = 2, PostCode = "2135" };
                });

        }

      
    }
}
