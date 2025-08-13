
namespace DNAPayments.AccountManagement.Infrastructure;

public class BankAccountDataGenerator : IBankAccountDataGenerator
{
    private readonly Random random;
    public BankAccountDataGenerator()
    {
        random = new Random();
    }
    public async Task<string> GenerateAccountNumber()
    {
        await Task.Delay(100);
        return random.NextInt64(100000000000, 999999999999).ToString();
    }

    public async Task<string> GenerateIBAN(string accountNumber)
    {
        await Task.Delay(100);
        return $"KZ{random.Next(10, 99)}125C{accountNumber}";
    }
}