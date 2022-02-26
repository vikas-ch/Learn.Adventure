using System.ComponentModel.DataAnnotations;

namespace Learn.Adventure.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First name is mandatory")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }
    }
}