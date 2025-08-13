namespace DNAPayments.AccountManagement.Infrastructure;

public static class InMemoryDbContextExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> collection)
    {
        await Task.Delay(100);
        return collection.ToList();
    }

    public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> queryable, Func<T, bool> func)
    {
        await Task.Delay(100);
        return queryable.FirstOrDefault(func);
    }

    public static async Task<bool> AnyAsync<T>(this IQueryable<T> queryable, Func<T, bool> func)
    {
        await Task.Delay(100);
        return queryable.Any(func);
    }

    public static async Task<T> AddAsync<T>(this IEnumerable<T> enumerable, T value)
    {
        enumerable.Append(value);
        await Task.Delay(100);
        return value;
    }
}