using DNAPayments.AccountManagement.Domain;
using DNAPayments.AccountManagement.Infrastructure;

public class BankAccountTypeRepository : IBankAccountTypeRepository
{
    private readonly IJsonDbClient _dbClient;

    public BankAccountTypeRepository(
        string dbName,
        Func<string, IJsonDbClient> dbClient)
    {
        _dbClient = dbClient(dbName);
    }
    public async Task<BankAccountType> Get(string accountType)
    {
        var source = await _dbClient.ReadDataAsync<BankAccountType>();
        return source.FirstOrDefault(x => x.Code.Equals(accountType, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<List<BankAccountType>> List()
    {
        var source = await _dbClient.ReadDataAsync<BankAccountType>();
        return source.ToList();
    }

}