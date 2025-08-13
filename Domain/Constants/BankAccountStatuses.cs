namespace DNAPayments.AccountManagement.Domain;

public class BankAccountStatuses
{
    public const string ACTIVE = "ACTIVE";
    public const string FROZEN = "FROZEN";
    
    public static List<string> GetStatuses() =>
        new () { ACTIVE, FROZEN };
}