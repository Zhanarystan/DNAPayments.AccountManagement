using System.ComponentModel.DataAnnotations;

namespace DNAPayments.AccountManagement.Application;

public class BankAccountListRequest
{
    public string? AccountType { get; set; }
    public int Page { get; set; } = 0;
    public int RecordCount { get; set; } = 10;
}