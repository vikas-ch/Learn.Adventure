using Learn.Adventure.Attributes;
using Learn.Adventure.Models.Abstractions;

namespace Learn.Adventure.Models.Entities
{
    [Collection("user")]
    public class User : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}