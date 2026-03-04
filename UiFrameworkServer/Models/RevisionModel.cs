namespace UiFrameworkServer.Models
{
    public class RevisionModel
    {
        public string Id { get; set; } = string.Empty;
        public required string ProjectId { get; set; }
        public required string DesignSnapshotId { get; set; }
        public required string ComponentManifestId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string? ParentRevisionId { get; set; }
        public string? ChildrenRevisionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
