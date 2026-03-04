namespace UiFrameworkServer.Models
{
    public class PageSnapshotModel
    {
        public string Id { get; set; } = string.Empty;
        public string RevisionId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string SpaceId { get; set; } = string.Empty;
        public string PageId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public string RootPageTreeId { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
