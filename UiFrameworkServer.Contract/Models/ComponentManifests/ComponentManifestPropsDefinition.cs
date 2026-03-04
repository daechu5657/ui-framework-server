namespace UiFrameworkServer.Contract.Models.ComponentManifests
{
    public abstract class ComponentManifestPropsDefinition
    {
        public abstract Enums.ComponentManifests.ComponentManifestPropsKind Kind { get; }
    }

    public sealed class ComponentManifestPropsStylePropertyDefinition
        : ComponentManifestPropsDefinition
    {
        public override Enums.ComponentManifests.ComponentManifestPropsKind Kind =>
            Enums.ComponentManifests.ComponentManifestPropsKind.Style;
        public List<ComponentManifestPropsStyleDefinition> Value { get; set; } = [];
    }

    public sealed class ComponentManifestPropsBehaviorPropertyDefinition
        : ComponentManifestPropsDefinition
    {
        public override Enums.ComponentManifests.ComponentManifestPropsKind Kind =>
            Enums.ComponentManifests.ComponentManifestPropsKind.Behavior;
        public List<ComponentManifestPropsBehaviorDefinition> Value { get; set; } = [];
    }
}
