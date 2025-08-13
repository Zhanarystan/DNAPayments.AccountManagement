using Newtonsoft.Json;

namespace DNAPayments.AccountManagement.Infrastructure;

public class JsonDbClient<T> : IJsonDbClient
{
    private readonly string _dbFileName;

    public JsonDbClient(string dbFileName)
    {
        _dbFileName = Path.Combine(Directory.GetCurrentDirectory(), dbFileName);
        if (!File.Exists(dbFileName))
            File.Create(_dbFileName).Close();
    }
    public async Task<IEnumerable<T>> ReadDataAsync<T>()
    {
        var jsonData = File.ReadAllText(_dbFileName);
        return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
    }

    public async Task<bool> WriteDataAsync<T>(T data)
    {
        var readData = await File.ReadAllTextAsync(_dbFileName);
        var records = JsonConvert.DeserializeObject<List<T>>(readData) ?? new List<T>();
        records.Add(data);
        var writeData = JsonConvert.SerializeObject(records);
        await File.WriteAllTextAsync(_dbFileName, writeData);
        return true;
    }

    public async Task<bool> UpdateDataAsync<T>(Func<T, bool> searchPredicate, T data)
    {
        var readData = await File.ReadAllTextAsync(_dbFileName);
        var records = JsonConvert.DeserializeObject<List<T>>(readData) ?? new List<T>();
        var target = records.FirstOrDefault(searchPredicate);
        if (target is  null)
            return false;
        records.Remove(target);
        records.Add(data);
        var writeData = JsonConvert.SerializeObject(records);
        await File.WriteAllTextAsync(_dbFileName, writeData);
        return true;
    }
}