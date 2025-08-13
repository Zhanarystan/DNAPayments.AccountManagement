using DNAPayments.AccountManagement.Domain;

namespace DNAPayments.AccountManagement.Application;

public interface ITokenService
{
    string CreateToken(User user);

}