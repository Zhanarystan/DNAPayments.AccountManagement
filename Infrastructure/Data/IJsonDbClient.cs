namespace DNAPayments.AccountManagement.Infrastructure;

public interface IJsonDbClient
{
    Task<IEnumerable<T>> ReadDataAsync<T>();
    Task<bool> WriteDataAsync<T>(T data);
    Task<bool> UpdateDataAsync<T>(Func<T, bool> searchPredicate, T data);
}