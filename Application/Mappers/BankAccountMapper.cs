using DNAPayments.AccountManagement.Domain;

namespace DNAPayments.AccountManagement.Application;

public static class BankAccountMapper
{
    public static BankAccountResponse ToResponse(this BankAccount bankAccount, User user, BankAccountType accountType)
    {
        return new BankAccountResponse
        {
            Id = bankAccount.Id,
            AccountNumber = bankAccount.AccountNumber,
            Balance = bankAccount.Balance,
            Currency = bankAccount.Currency,
            Status = bankAccount.Status,
            IBAN = bankAccount.IBAN,
            CreatedAt = bankAccount.CreatedAt,
            OwnerFirstName = user.FirstName,
            OwnerLastName = user.LastName,
            OwnerMiddleName = user.MiddleName,
            AccountType = accountType.Name
        };
    }
} 