using DNAPayments.AccountManagement.Domain;
using DNAPayments.AccountManagement.Infrastructure;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly IJsonDbClient _dbClient;

    public CurrencyRepository(
        string dbName,
        Func<string, IJsonDbClient> dbClient)
    {
        _dbClient = dbClient(dbName);
    }

    public async Task<bool> IsValid(string currencyCode)
    {
        var source = await _dbClient.ReadDataAsync<string>();
        return source.Any(x =>x.Equals(currencyCode, StringComparison.OrdinalIgnoreCase));
    }
}