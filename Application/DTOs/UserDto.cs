namespace DNAPayments.AccountManagement.Application;

public class UserDto
{
    public UserDto(string phoneNumber, string token)
    {
        PhoneNumber = phoneNumber;
        Token = token;
    }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    
}