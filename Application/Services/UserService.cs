using DNAPayments.AccountManagement.Domain;
using Microsoft.Extensions.Logging;

namespace DNAPayments.AccountManagement.Application;

public class UserService(
    ITokenService tokenService,
    IUserRepository userRepository,
    ILogger<UserService> logger) : IUserService
{
    public async Task<Result<UserDto>> Register(RegisterRequest request)
    {
        if (await userRepository.Exists(request.PhoneNumber))
        {
            logger.LogWarning(
                $"Phone number {request.PhoneNumber} is taken",
                nameof(UserService),
                nameof(Register));
            return Result<UserDto>.Failure($"Phone number {request.PhoneNumber} is taken");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            PhoneNumber = request.PhoneNumber,
            IIN = request.IIN,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Address = request.Address,
            Status = UserStatuses.ACTIVE
        };

        var resultedUser = await userRepository.Create(user);

        var token = tokenService.CreateToken(user);

        return Result<UserDto>.Success(new UserDto(resultedUser.PhoneNumber, token));
    }

    public async Task<Result<UserDto>> Login(LoginRequest request)
    {
        var user = await userRepository.Get(request.PhoneNumber);

        if (user is null)
        {
            logger.LogWarning(
                $"User not found by phone number {request.PhoneNumber}",
                nameof(UserService),
                nameof(Login));
            return Result<UserDto>.Failure("Invalid user");
        }

        if (!user.Password.Equals(request.Password))
        {
            logger.LogWarning(
                $"Wrong password typed by user {user.Id}",
                nameof(UserService),
                nameof(Login));
            return Result<UserDto>.Failure("Wrong password");
        }

        logger.LogInformation(
            $"User {user.Id} successfully authenticated",
            nameof(UserService),
            nameof(Login));

        return Result<UserDto>.Success(new UserDto(request.PhoneNumber, tokenService.CreateToken(user)));
    }

}