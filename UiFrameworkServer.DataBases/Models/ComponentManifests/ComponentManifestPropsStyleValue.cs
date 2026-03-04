using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models.ComponentManifests
{
    public class ComponentManifestPropsStyleValue
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DesignTokenId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? DesignTokenValueId { get; set; }

        public required Enums.ComponentManifests.ComponentManifestPropsStyleValueKind Kind { get; set; }
        public required Enums.Designs.StyleValueType ValueType { get; set; }
        public string? StringValue { get; set; }
        public int? NumberValue { get; set; }
    }
}
