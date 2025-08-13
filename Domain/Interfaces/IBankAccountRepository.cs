using DNAPayments.AccountManagement.Domain;

public interface IBankAccountRepository
{
    Task<bool> Create(BankAccount bankAccount);
    Task<List<BankAccount>> List(string accountType = "");
    Task<BankAccount> Get(Guid id);
    Task<bool> Update(BankAccount bankAccount);
}