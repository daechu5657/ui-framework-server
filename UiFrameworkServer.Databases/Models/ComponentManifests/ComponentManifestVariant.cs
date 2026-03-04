using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models.ComponentManifests
{
    public class ComponentManifestVariant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ProjectId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string ComponentManifestId { get; set; }

        public required List<ComponentManifestProps> PropsOverride { get; set; }
        public required string Key { get; set; }
        public required string Name { get; set; }
        public int Order { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; }
    }
}
