using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using SimpleGraphQL.GraphQLSchema;
using System;
using System.Linq;
using System.Reflection;


namespace SimpleGraphQL.Extensions
{
    public static class GraphQLExtensions
    {
        public static IServiceCollection ConfigureGraphQLTypes(this IServiceCollection services)
        {


          
            (from t in Assembly.GetAssembly(typeof(UserDetailsSchema)).GetTypes()
             where
                 t.BaseType.IsGenericType
                 &&
                    (t.BaseType.GetGenericTypeDefinition() == typeof(AutoRegisteringObjectGraphType<>) ||
                    t.BaseType.GetGenericTypeDefinition() == typeof(AutoRegisteringInputObjectGraphType<>) ||
                    t.BaseType.GetGenericTypeDefinition() == typeof(ListGraphType<>) ||
                    t.BaseType.GetGenericTypeDefinition() == typeof(EnumerationGraphType<>))
                    select t).ToList().ForEach(t => services.AddSingleton(t)
             );


            (from t in Assembly.GetAssembly(typeof(UserDetailsSchema)).GetTypes()
             where
                 (!t.IsAbstract && !t.IsInterface) &&
                 (t.IsSubclassOf(typeof(ObjectGraphType)) || t.IsSubclassOf(typeof(ObjectGraphType<>)))
             select t).ToList().ForEach(t => services.AddSingleton(t)
             );

            services.AddSingleton<ISchema, UserDetailsSchema>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddGraphQL((options, provider) =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = (ctx) =>
                {
                    throw new Exception(ctx.OriginalException.Message);
                };
            }).AddNewtonsoftJson();
            return services;
        }
    }
}
