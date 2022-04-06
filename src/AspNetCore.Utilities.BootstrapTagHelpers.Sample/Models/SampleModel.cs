using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Utilities.BootstrapTagHelpers.Sample.Models
{
    public class SampleModel
    {
        [Display(Name = "Your Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = "";

        public string AdditionalInfo { get; set; } = "";
    }
}
