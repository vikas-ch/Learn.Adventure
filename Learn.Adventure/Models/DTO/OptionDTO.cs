using System.ComponentModel.DataAnnotations;

namespace Learn.Adventure.Models.DTO
{
    public class OptionDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Adventure Id is mandatory")]
        public string AdventureId { get; set; }
        public string ParentId { get; set; }
        [Required(ErrorMessage = "Option text cannot be left blank")]
        public string Text { get; set; }
    }
}