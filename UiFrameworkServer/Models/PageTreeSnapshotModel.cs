namespace UiFrameworkServer.Models
{
    public class PageTreeSnapshotModel
    {
        public string Id { get; set; } = string.Empty;
        public string RevisionId { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string SpaceId { get; set; } = string.Empty;
        public string PageId { get; set; } = string.Empty;
        public string PageTreeId { get; set; } = string.Empty;
        public string ComponentManifestId { get; set; } = string.Empty;
        public string VariantId { get; set; } = string.Empty;
        public string? Parent { get; set; } = string.Empty;
        public List<string> Children { get; set; } = [];

        // public ComponentManifestProps[] PropsOverride { get; set; } = ComponentManifestProps[]
        public string Name { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public string RootPageTreeId { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
