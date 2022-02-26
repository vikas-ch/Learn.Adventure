using System.ComponentModel.DataAnnotations;

namespace Learn.Adventure.Models.DTO
{
    public class AdventureDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Adventure name is mandatory")]
        public string Name { get; set; }
    }
}