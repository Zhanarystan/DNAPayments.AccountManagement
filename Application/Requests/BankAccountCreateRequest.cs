using System.ComponentModel.DataAnnotations;

namespace DNAPayments.AccountManagement.Application;

public class BankAccountCreateRequest
{
    [Required]
    public string AccountType { get; set; }

    [Required]
    public string Currency { get; set; }
    
    public decimal InitialBalance { get; set; } = 0;
}