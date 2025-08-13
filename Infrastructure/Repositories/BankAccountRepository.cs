using System.Net.Http.Headers;
using DNAPayments.AccountManagement.Domain;

namespace DNAPayments.AccountManagement.Infrastructure;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly IJsonDbClient _dbClient;

    public BankAccountRepository(
        string dbName,
        Func<string, IJsonDbClient> dbClient)
    {
        _dbClient = dbClient(dbName);
    }
    public async Task<bool> Create(BankAccount bankAccount)
    {
        return await _dbClient.WriteDataAsync(bankAccount);
    }

    public async Task<List<BankAccount>> List(string accountType = "")
    {
        var source = await _dbClient.ReadDataAsync<BankAccount>();
        if (!string.IsNullOrWhiteSpace(accountType))
            return source
                .Where(x => x.AccountTypeCode.Equals(accountType, StringComparison.OrdinalIgnoreCase))
                .ToList();

        return source.ToList();
    }

    public async Task<BankAccount> Get(Guid id)
    {
        var source = await _dbClient.ReadDataAsync<BankAccount>();
        return source.FirstOrDefault(x => x.Id.Equals(id));
    }

    public async Task<bool> Update(BankAccount bankAccount)
    {
        return await _dbClient.UpdateDataAsync(x => x.Id.Equals(bankAccount.Id), bankAccount);
    }
}