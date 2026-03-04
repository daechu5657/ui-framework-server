using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models.ComponentManifests
{
    public class ComponentManifestPropsStyle
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public required List<string> DesignTokenIds { get; set; }

        public required string Key { get; set; }
        public required string Name { get; set; }
        public required string CssProperty { get; set; }
        public required Enums.Designs.StyleValueType ValueType { get; set; }
        public required ComponentManifestPropsStyleValue Value { get; set; }
        public Enums.Designs.StyleValueUnit? Unit { get; set; }
    }
}
