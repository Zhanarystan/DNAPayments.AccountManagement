using DNAPayments.AccountManagement.Domain;

public interface IBankAccountDataGenerator
{
    Task<string> GenerateAccountNumber();
    Task<string> GenerateIBAN(string accountNumber);
}