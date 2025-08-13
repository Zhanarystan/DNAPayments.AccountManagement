

using DNAPayments.AccountManagement.Application;
using DNAPayments.AccountManagement.Domain;
using DNAPayments.AccountManagement.Infrastructure;

namespace DNAPayments.AccountManagement.API;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBankAccountService, BankAccountService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IBankAccountRepository>(sp => 
            new BankAccountRepository(
                "bank-accounts", 
                sp.GetService<Func<string, IJsonDbClient>>()));
        
        services.AddScoped<IUserRepository>(sp => 
            new UserRepository(
                "users", 
                sp.GetService<Func<string, IJsonDbClient>>()));
        
        services.AddScoped<IBankAccountTypeRepository>(sp => 
            new BankAccountTypeRepository(
                "bank-account-types", 
                sp.GetService<Func<string, IJsonDbClient>>())); 
        
        services.AddScoped<ICurrencyRepository>(sp => 
            new CurrencyRepository(
                "currencies", 
                sp.GetService<Func<string, IJsonDbClient>>()));

        services.AddScoped<IBankAccountDataGenerator, BankAccountDataGenerator>();

        services.AddScoped<Func<string, IJsonDbClient>>(sp => key =>
            key.ToLower() switch
            {
                "bank-accounts" => new JsonDbClient<BankAccount>("bank-accounts.json"),
                "users" => new JsonDbClient<User>("users.json"),
                "bank-account-types" => new JsonDbClient<BankAccountType>("bank-account-types.json"),
                "currencies" => new JsonDbClient<string>("currencies.json")
            });
        return services;
    }
}