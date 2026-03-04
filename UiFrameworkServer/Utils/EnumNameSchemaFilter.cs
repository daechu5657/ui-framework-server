using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class EnumNameSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var enumType = Nullable.GetUnderlyingType(context.Type) ?? context.Type;
        if (!enumType.IsEnum)
        {
            return;
        }

        var names = Enum.GetNames(enumType);

        var openApiNames = new OpenApiArray();
        foreach (var name in names)
        {
            openApiNames.Add(new OpenApiString(name));
        }

        schema.Extensions["x-enum-varnames"] = openApiNames;
    }
}
