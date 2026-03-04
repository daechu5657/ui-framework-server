namespace UiFrameworkServer.Models
{
    public class PageModel
    {
        public string Id { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string SpaceId { get; set; } = string.Empty;
        public string RootPageTreeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; } = 0;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
