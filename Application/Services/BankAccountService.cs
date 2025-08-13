using System.IdentityModel.Tokens.Jwt;
using DNAPayments.AccountManagement.Domain;
using DNAPayments.AccountManagement.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DNAPayments.AccountManagement.Application;

public class BankAccountService(
    IBankAccountRepository bankAccountRepository,
    IBankAccountTypeRepository bankAccountTypeRepository,
    ICurrencyRepository currencyRepository,
    IUserRepository userRepository,
    IBankAccountDataGenerator bankAccountDataGenerator,
    IHttpContextAccessor httpContextAccessor,
    ILogger<BankAccountService> logger) : IBankAccountService
{
    public async Task<Result<BankAccount>> Create(BankAccountCreateRequest request)
    {
        var authenticatedUser = httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.PhoneNumber);
        var user = await userRepository.Get(authenticatedUser.Value);
        if (user is null)
        {
            logger.LogError(
                $"Authenticated user with phone number {authenticatedUser.Value} not found",
                nameof(BankAccountService),
                nameof(Create));
            return Result<BankAccount>.Failure($"Your phone number {authenticatedUser.Value} not found"); 
        }

        if (user.Status.Equals(UserStatuses.BANNED))
        {
            logger.LogInformation(
                $"The user with phone number {user.PhoneNumber} is banned",
                nameof(BankAccountService),
                nameof(Create));
            return Result<BankAccount>.Failure("You cannot open a new bank account");
        }

        var accountType = await bankAccountTypeRepository.Get(request.AccountType);

        if (accountType is null)
        {
            logger.LogWarning(
                $"Unexpected account type {request.AccountType}",
                nameof(BankAccountService),
                nameof(Create));
            return Result<BankAccount>.Failure("You cannot open a new bank account");
        }

        if (!await currencyRepository.IsValid(request.Currency))
        {
            logger.LogWarning(
                $"Invalid currency {request.AccountType}",
                nameof(BankAccountService),
                nameof(Create));
            return Result<BankAccount>.Failure("You cannot open a new bank account");
        }

        var accountNumber = await bankAccountDataGenerator.GenerateAccountNumber();
        var iban = await bankAccountDataGenerator.GenerateIBAN(accountNumber);
        var bankAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            Balance = request.InitialBalance,
            Currency = request.Currency,
            Status = BankAccountStatuses.ACTIVE,
            IBAN = iban,
            CreatedAt = DateTime.Now,
            UserId = user.Id,
            AccountTypeCode = accountType.Code
        };
        
        if (!await bankAccountRepository.Create(bankAccount))
            return Result<BankAccount>.Failure("Failed to create bank account");
        return Result<BankAccount>.Success(bankAccount);
    }

    public async Task<Result<PagedList<BankAccountResponse>>> List(BankAccountListRequest request)
    {
        var bankAccounts = await bankAccountRepository.List(request.AccountType);

        logger.LogInformation(
            "Bank accounts queried",
            nameof(BankAccountService),
            nameof(List),
            new { Data = request });

        var accountTypes = await bankAccountTypeRepository.List();
        var users = await userRepository.List();
        var detailedBankAccounts = bankAccounts
                                    .Join(
                                        accountTypes,
                                        account => account.AccountTypeCode,
                                        type => type.Code,
                                        (account, type) => (account, type))
                                    .Join(
                                        users,
                                        bankAccount => bankAccount.account.UserId,
                                        owner => owner.Id,
                                        (bankAccount, owner) => bankAccount.account.ToResponse(owner, bankAccount.type));

        var pagedList = PagedList<BankAccountResponse>.Create(detailedBankAccounts, request.Page, request.RecordCount);

        logger.LogInformation(
            $"List of bank accounts with {pagedList.TotalCount} retrieved",
            nameof(BankAccountService),
            nameof(List),
            new { Data = request });

        return Result<PagedList<BankAccountResponse>>.Success(pagedList);
    }

    public async Task<Result<BankAccount>> ChangeStatus(BankAccountChangeStatusRequest request)
    {
        var status = BankAccountStatuses
            .GetStatuses()
            .FirstOrDefault(x => x.Equals(request.Status, StringComparison.OrdinalIgnoreCase));
        if (status is null)
        {
            return Result<BankAccount>.Failure("Invalid status");
        }

        var bankAccount = await bankAccountRepository.Get(request.Id);
        bankAccount.Status = status;
        if (!await bankAccountRepository.Update(bankAccount))
        {
            return Result<BankAccount>.Failure("Failed to update bank account");
        }
        return Result<BankAccount>.Success(bankAccount);
    }

}
