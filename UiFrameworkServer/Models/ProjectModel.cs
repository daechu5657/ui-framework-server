namespace UiFrameworkServer.Models
{
    public class ProjectModel
    {
        public string Id { get; set; } = string.Empty;
        public string HeadRevisionId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
