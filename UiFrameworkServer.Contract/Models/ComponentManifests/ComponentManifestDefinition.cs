namespace UiFrameworkServer.Contract.Models.ComponentManifests
{
    public class ComponentManifestDefinition
    {
        public required string Name { get; set; }
        public required string TagName { get; set; }
        public required List<ComponentManifestPropsDefinition> BaseProps { get; set; }
        public required List<string> Variants { get; set; }

        public Dictionary<
            string,
            List<ComponentManifestPropsDefinition>
        > VariantOverrides { get; set; } = new();
    }
}
