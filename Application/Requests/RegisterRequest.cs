using System.ComponentModel.DataAnnotations;

namespace DNAPayments.AccountManagement.Application;

public class RegisterRequest
{
    [Required(ErrorMessage = "Phone number required")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Login must consist of exactly 10 digits")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Login must be a valid Kazakhstani phone number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "IIN required")]
    [StringLength(12, MinimumLength = 12, ErrorMessage = "IIN must consist of exactly 12 digits")]
    public string IIN { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }

    [Required(ErrorMessage = "First name required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name required")]
    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? Address { get; set; }

}
