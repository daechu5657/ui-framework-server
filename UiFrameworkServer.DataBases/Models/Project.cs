using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UiFrameworkServer.Databases.Models
{
    public class Project
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required string Name { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; }
    }
}
