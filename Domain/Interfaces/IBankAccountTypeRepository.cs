using DNAPayments.AccountManagement.Domain;

public interface IBankAccountTypeRepository
{
    Task<List<BankAccountType>> List();
    Task<BankAccountType> Get(string accountType);
}