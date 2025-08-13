

using DNAPayments.AccountManagement.Domain;

namespace DNAPayments.AccountManagement.Application;

public interface IBankAccountService
{
    Task<Result<BankAccount>> Create(BankAccountCreateRequest request);
    Task<Result<BankAccount>> ChangeStatus(BankAccountChangeStatusRequest request);
    Task<Result<PagedList<BankAccountResponse>>> List(BankAccountListRequest request);
}