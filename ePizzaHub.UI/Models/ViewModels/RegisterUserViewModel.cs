using System.ComponentModel.DataAnnotations;

namespace ePizzaHub.UI.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password should be of minimum 5 characters")]
        [MaxLength(15, ErrorMessage = "Password should be of maximum 15 characters")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password should be of minimum 5 characters")]
        [MaxLength(15, ErrorMessage = "Password should be of maximum 15 characters")]
        public string ConfirmPassword { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = default!;
    }
}
