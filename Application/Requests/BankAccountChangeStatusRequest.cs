using System.ComponentModel.DataAnnotations;

namespace DNAPayments.AccountManagement.Application;

public class BankAccountChangeStatusRequest
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Status { get; set; }
}