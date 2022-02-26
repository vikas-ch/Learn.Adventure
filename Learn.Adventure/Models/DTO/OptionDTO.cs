using Learn.Adventure.Models.Common;

namespace Learn.Adventure.Models.DTO
{
    public class OptionDTO
    {
        public string Id { get; set; }
        public string AdventureId { get; set; }
        // public Node Node { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
    }
}