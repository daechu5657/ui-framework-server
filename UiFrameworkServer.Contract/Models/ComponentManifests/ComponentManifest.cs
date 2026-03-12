using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Contract.Models.ComponentManifests
{
    public class ComponentManifest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ProjectId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string DefaultVariantId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required List<string> VariantIds { get; set; }

        public required List<ComponentManifestProps> BaseProps { get; set; }
        public required string TagName { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; }
    }
}
