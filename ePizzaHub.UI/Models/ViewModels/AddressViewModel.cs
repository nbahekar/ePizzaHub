using System.ComponentModel.DataAnnotations;

namespace ePizzaHub.UI.Models.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Street should not be more than 100 characters")]
        public string Street { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Locality should not be more than 100 characters")]
        public string Locality { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "ZipCode should not be more than 10 characters")]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "City should not be more than 50 characters")]
        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
