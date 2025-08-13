

using DNAPayments.AccountManagement.Infrastructure;

namespace DNAPayments.AccountManagement.Application;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public static PagedList<T> Create(IEnumerable<T> source, int pageNumber,
        int pageSize)
    {
        var query = source.Skip(pageNumber * pageSize).Take(pageSize);
        return new PagedList<T>(query, source.Count(), pageNumber, pageSize);
    }
}