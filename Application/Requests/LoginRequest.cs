using System.ComponentModel.DataAnnotations;

namespace DNAPayments.AccountManagement.Application;

public class LoginRequest
{
    [Required(ErrorMessage = "Phone number required")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Login must consist of exactly 10 digits")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Login must be a valid Kazakhstani phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}