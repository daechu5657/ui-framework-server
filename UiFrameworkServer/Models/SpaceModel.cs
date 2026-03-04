namespace UiFrameworkServer.Models
{
    public class SpaceModel
    {
        public string Id { get; set; } = string.Empty;
        public string ProjectId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; } = 0;
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
