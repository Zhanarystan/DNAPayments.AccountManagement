using DNAPayments.AccountManagement.Domain;
using Microsoft.Extensions.Logging;

namespace DNAPayments.AccountManagement.Infrastructure;

public class UserRepository : IUserRepository
{
    
    private readonly IJsonDbClient _dbClient;

    public UserRepository(
        string dbName,
        Func<string, IJsonDbClient> dbClient)
    {
        _dbClient = dbClient(dbName);
    }
    public async Task<User> Create(User user)
    {
        if (await _dbClient.WriteDataAsync(user))
            return user;
        return null;
    }

    public async Task<bool> Exists(string phoneNumber)
    {
        var source = await _dbClient.ReadDataAsync<User>();
        return source.Any(u => u.PhoneNumber.Equals(phoneNumber));
    }

    public async Task<User> Get(string phoneNumber)
    {
        var source = await _dbClient.ReadDataAsync<User>();
        return source.FirstOrDefault(u => u.PhoneNumber.Equals(phoneNumber));
    }

    public async Task<List<User>> List()
    {
        var source = await _dbClient.ReadDataAsync<User>();
        return source.ToList();
    }

}