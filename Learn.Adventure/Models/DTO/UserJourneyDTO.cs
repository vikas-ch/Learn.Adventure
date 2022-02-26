

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Learn.Adventure.Models.DTO
{
    public class UserJourneyDTO
    {
        [Required(ErrorMessage = "User Id is mandatory")]
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Adventure Id is mandatory")]
        public string AdventureId { get; set; }
        public List<string> SelectedOptions { get; set; }
    }
}