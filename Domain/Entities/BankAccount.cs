namespace DNAPayments.AccountManagement.Domain;

public class BankAccount
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string IBAN { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
    public string AccountTypeCode { get; set; }
}