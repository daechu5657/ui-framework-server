using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using UiFrameworkServer.Contract.Enums.ComponentManifests;
using UiFrameworkServer.Contract.Models.ComponentManifests;

public sealed class ComponentManifestPolymorphismSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ComponentManifestPropsDefinition))
        {
            ApplyDiscriminator(
                schema,
                "ComponentManifestPropsStylePropertyDefinition",
                "ComponentManifestPropsBehaviorPropertyDefinition"
            );
            return;
        }

        if (context.Type == typeof(ComponentManifestProps))
        {
            ApplyDiscriminator(
                schema,
                "ComponentManifestPropsStyleProperty",
                "ComponentManifestPropsBehaviorProperty"
            );
            return;
        }

        if (TryGetFixedKind(context.Type, out var kind))
        {
            ApplyFixedKind(schema, kind);
        }
    }

    private static void ApplyDiscriminator(
        OpenApiSchema schema,
        string styleSchemaName,
        string behaviorSchemaName
    )
    {
        schema.Discriminator = new OpenApiDiscriminator
        {
            PropertyName = "kind",
            Mapping =
            {
                [ComponentManifestPropsKind.Style.ToString()] =
                    $"#/components/schemas/{styleSchemaName}",
                [ComponentManifestPropsKind.Behavior.ToString()] =
                    $"#/components/schemas/{behaviorSchemaName}",
            },
        };
    }

    private static void ApplyFixedKind(OpenApiSchema schema, ComponentManifestPropsKind kind)
    {
        if (!schema.Properties.TryGetValue("kind", out var kindSchema))
        {
            kindSchema = new OpenApiSchema();
            schema.Properties["kind"] = kindSchema;
        }

        kindSchema.Reference = null;
        kindSchema.Type = "integer";
        kindSchema.Format = "int32";
        kindSchema.Enum = [new OpenApiInteger((int)kind)];

        var enumNames = new OpenApiArray();
        enumNames.Add(new OpenApiString(kind.ToString()));
        kindSchema.Extensions["x-enum-varnames"] = enumNames;

        schema.Required ??= new HashSet<string>();
        schema.Required.Add("kind");
    }

    private static bool TryGetFixedKind(Type type, out ComponentManifestPropsKind kind)
    {
        if (
            type == typeof(ComponentManifestPropsStyleProperty)
            || type == typeof(ComponentManifestPropsStylePropertyDefinition)
        )
        {
            kind = ComponentManifestPropsKind.Style;
            return true;
        }

        if (
            type == typeof(ComponentManifestPropsBehaviorProperty)
            || type == typeof(ComponentManifestPropsBehaviorPropertyDefinition)
        )
        {
            kind = ComponentManifestPropsKind.Behavior;
            return true;
        }

        kind = default;
        return false;
    }
}
