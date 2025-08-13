

using DNAPayments.AccountManagement.Domain;

namespace DNAPayments.AccountManagement.Application;

public interface IUserService
{
    Task<Result<UserDto>> Register(RegisterRequest request);
    Task<Result<UserDto>> Login(LoginRequest request);
}