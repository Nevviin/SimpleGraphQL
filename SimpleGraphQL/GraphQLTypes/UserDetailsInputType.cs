using SimpleGraphQL.Models;
using GraphQL.Types;

namespace SimpleGraphQL.GraphQLTypes
{
    public class UserDetailsInputType:AutoRegisteringInputObjectGraphType<UserDetailsInputModel>
    {
        public UserDetailsInputType()
        {

        }
    }
}
