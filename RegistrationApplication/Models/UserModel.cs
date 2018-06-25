using System.ComponentModel.DataAnnotations;

namespace RegistrationApplication.Models
{
    public class UserModel
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(320, ErrorMessage = "Email Address must not exceed 320 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Passwords must be between 8 and 32 characters")]
        public string Password { get; set; }
    }
}