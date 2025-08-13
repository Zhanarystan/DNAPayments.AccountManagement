using DNAPayments.AccountManagement.Domain;

public interface ICurrencyRepository
{
    Task<bool> IsValid(string currencyCode);
}