using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models.Designs
{
    public class DesignTokenValueSnapshot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string DesignSnapshotId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string DesignTokenSnapshotId { get; set; }

        public required string Name { get; set; }
        public string? StringValue { get; set; }
        public int? NumberValue { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; }
    }
}
