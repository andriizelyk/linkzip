
using System.Collections.Concurrent;

internal class MemoryStorage : IStorage
{
    private static readonly ConcurrentDictionary<string, (string, DateTime)> _storage = new ConcurrentDictionary<string, (string, DateTime)>();

    public Task AddAsync(string key, string value, DateTime expiryDate)
    {
        var result = !_storage.ContainsKey(key) && _storage.TryAdd(key, (value, expiryDate));

        if (!result)
            throw new ArgumentException("Value with key can not be added", nameof(key));

        return Task.CompletedTask;
    }

    public Task<bool> ContainsKeyAsync(string key) => Task.FromResult(_storage.ContainsKey(key));
    

    public Task<string> GetByKeyAsync(string key)
    {
        var result = _storage.GetValueOrDefault(key);

        var isExpired = result.Item2 < DateTime.UtcNow;
        if (isExpired)
        {
            _storage.Remove(key, out var value);
            return null;
        }

        return Task.FromResult(result.Item1);
    }
}