using System.Collections.Generic;
using System.Web.Http.Description;

namespace WebAnime.API2.Components
{
    public class HeaderFilter : Swagger.Net.IOperationFilter
    {
        public void Apply(Swagger.Net.Operation operation, Swagger.Net.SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Swagger.Net.Parameter>();
            }

            operation.parameters.Add(new Swagger.Net.Parameter()
            {
                name = "Authorization",
                @in = "header",
                type = "string",
                required = false
            });
        }
    }
}