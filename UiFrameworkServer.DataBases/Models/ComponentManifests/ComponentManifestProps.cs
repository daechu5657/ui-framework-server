using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models.ComponentManifests
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(
        typeof(ComponentManifestPropsStyleProperty),
        typeof(ComponentManifestPropsBehaviorProperty)
    )]
    public abstract class ComponentManifestProps
    {
        public abstract Enums.ComponentManifests.ComponentManifestPropsKind Kind { get; }
    }

    public sealed class ComponentManifestPropsStyleProperty : ComponentManifestProps
    {
        public override Enums.ComponentManifests.ComponentManifestPropsKind Kind =>
            Enums.ComponentManifests.ComponentManifestPropsKind.Style;
        public List<ComponentManifestPropsStyle> Value { get; set; } = [];
    }

    public sealed class ComponentManifestPropsBehaviorProperty : ComponentManifestProps
    {
        public override Enums.ComponentManifests.ComponentManifestPropsKind Kind =>
            Enums.ComponentManifests.ComponentManifestPropsKind.Behavior;
        public List<ComponentManifestPropsBehavior> Value { get; set; } = [];
    }
}
