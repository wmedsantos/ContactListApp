using System;
using System.Threading.Tasks;

namespace ContactList.Infrastructure
{
    public interface IRedisService
    {
        Task<string> GetAsync(string collection, string key);
        Task SetAsync(string collection, string key, string value);
        Task RemoveAsync(string collection, string key);
        Task<IEnumerable<string>> GetAllAsync(string collection);
    }
}
