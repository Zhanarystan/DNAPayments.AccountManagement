using DNAPayments.AccountManagement.Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DNAPayments.AccountManagement.API;

[Route("api/v1/user-account")]
public class UserAccountController(
    IUserService userService) : BaseApiController
{
    [HttpPost("register")]
    [EndpointName(nameof(Register))]
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            var serializableModelState = new SerializableError(ModelState);
            throw new InvalidRequestException(
                JsonConvert.SerializeObject(serializableModelState),
                "Invalid user registration request");
        }

        return HandleResult(await userService.Register(request));
    }

    [HttpPost("login")]
    [EndpointName(nameof(Login))]
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            var serializableModelState = new SerializableError(ModelState);
            throw new InvalidRequestException(
                JsonConvert.SerializeObject(serializableModelState),
                "Invalid user login request");
        }

        return HandleResult(await userService.Login(request));
    }
}