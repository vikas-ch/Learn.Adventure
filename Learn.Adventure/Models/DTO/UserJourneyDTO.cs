

using System.Collections.Generic;

namespace Learn.Adventure.Models.DTO
{
    public class UserJourneyDTO
    {
        public string UserId { get; set; }
        public string AdventureId { get; set; }
        // public UserSelectedNode Node { get; set; }
        public List<string> SelectedOptions { get; set; }
    }
}