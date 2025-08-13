using DNAPayments.AccountManagement.Domain;

public interface IUserRepository
{
    Task<List<User>> List();
    Task<User> Create(User user);
    Task<User> Get(string phoneNumber);
    Task<bool> Exists(string phoneNumber);
}