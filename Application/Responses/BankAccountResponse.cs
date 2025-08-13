namespace DNAPayments.AccountManagement.Application;

public class BankAccountResponse
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string IBAN { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OwnerFirstName { get; set; }
    public string OwnerLastName { get; set; }
    public string OwnerMiddleName { get; set; }
    public string AccountType { get; set; }
}