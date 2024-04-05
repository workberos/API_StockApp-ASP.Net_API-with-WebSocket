using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Stock.ViewModes
{
    public class RegisterViewModel
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = "";
        public string? Fullname { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Country { get; set; }
    }
}
