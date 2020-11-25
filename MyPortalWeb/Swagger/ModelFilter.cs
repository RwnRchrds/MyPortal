using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyPortalWeb.Swagger
{
    public class ModelFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var keys = new List<string>();
            var prefix = "MyPortal.Database";
            foreach (var key in context.SchemaRepository.Schemas.Keys)
            {
                if (key.StartsWith(prefix))
                {
                    keys.Add(key);
                }
            }
            foreach (var key in keys)
            {
                context.SchemaRepository.Schemas.Remove(key);
            }
        }
    }
}
