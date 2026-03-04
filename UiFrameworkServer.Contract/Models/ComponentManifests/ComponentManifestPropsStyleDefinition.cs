namespace UiFrameworkServer.Contract.Models.ComponentManifests
{
    public class ComponentManifestPropsStyleDefinition
    {
        public required string Key { get; set; }
        public required string Name { get; set; }
        public required string CssProperty { get; set; }
        public required Enums.Designs.StyleValueType ValueType { get; set; }
        public Enums.Designs.StyleValueUnit? Unit { get; set; }
    }
}
