using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Stock.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Username length can't be more than 100 characters")]
        [Column("username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(200, ErrorMessage = "Password length can't be more than 200 characters")]
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(255, ErrorMessage = "Email length can't be more than 255 characters")]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid Phone Number")]
        [Column("phone")]
        public string? Phone { get; set; }

        [StringLength(255, ErrorMessage = "Full name length can't be more than 255 characters")]
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200, ErrorMessage = "Country length can't be more than 200 characters")]
        [Column("country")]
        public string? Country { get; set; }
        public ICollection<Watchlist>? Watchlists { get; set; }
    } 
}
