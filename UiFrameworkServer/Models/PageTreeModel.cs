namespace UiFrameworkServer.Models
{
    public class PageTreeModel
    {
        public string Id { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string PageId { get; set; } = string.Empty;
        public string ComponentManifestId { get; set; } = string.Empty;
        public string VariantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; } = 0;
        public string? Parent { get; set; } = string.Empty;
        public List<string> Children { get; set; } = [];

        // public ComponentManifestProps[] PropsOverride { get; set; } = ComponentManifestProps[]
        //
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
