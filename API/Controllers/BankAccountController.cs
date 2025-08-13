using System.Text.Json.Serialization;
using DNAPayments.AccountManagement.Application;
using DNAPayments.AccountManagement.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DNAPayments.AccountManagement.API;

[Authorize]
[Route("api/v1/bank-account")]
public class BankAccountController(
    IBankAccountService bankAccountService
) : BaseApiController
{

    [HttpPost]
    [EndpointName(nameof(Create))]
    [ProducesResponseType(typeof(BankAccount), 200)]
    public async Task<IActionResult> Create([FromBody] BankAccountCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            var serializableModelState = new SerializableError(ModelState);
            throw new InvalidRequestException(
                JsonConvert.SerializeObject(serializableModelState),
                "Invalid bank account creation request");
        }
        return HandleResult(await bankAccountService.Create(request));
    }

    [HttpGet]
    [EndpointName(nameof(List))]
    [ProducesResponseType(typeof(Result<PagedList<BankAccount>>), 200)]
    public async Task<IActionResult> List([FromQuery] BankAccountListRequest request)
    {
        if (!ModelState.IsValid)
        {
            var serializableModelState = new SerializableError(ModelState);
            throw new InvalidRequestException(
                JsonConvert.SerializeObject(serializableModelState),
                "Invalid bank account list request");
        }
        return HandlePagedResult(await bankAccountService.List(request));
    }

    [HttpPatch("status")]
    [EndpointName(nameof(ChageStatus))]
    [ProducesResponseType(typeof(Result<BankAccount>), 200)]
    public async Task<IActionResult> ChageStatus([FromBody] BankAccountChangeStatusRequest request)
    {
        if (!ModelState.IsValid)
        {
            var serializableModelState = new SerializableError(ModelState);
            throw new InvalidRequestException(
                JsonConvert.SerializeObject(serializableModelState),
                "Invalid bank account list request");
        }
        return HandleResult(await bankAccountService.ChangeStatus(request));
    }
}