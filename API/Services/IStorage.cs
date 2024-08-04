public interface IStorage 
{
    Task AddAsync(string key, string value, DateTime expiryDate);

    Task<bool> ContainsKeyAsync(string key);

    Task<string> GetByKeyAsync(string key);
}