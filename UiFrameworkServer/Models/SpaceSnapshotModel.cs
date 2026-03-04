namespace UiFrameworkServer.Models
{
    public class SpaceSnapshotModel
    {
        public string Id { get; set; } = string.Empty;
        public string RevisionId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string SpaceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public string[] Pages { get; set; } = [];
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
