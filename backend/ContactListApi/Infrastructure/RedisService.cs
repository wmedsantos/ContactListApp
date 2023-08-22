using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ContactList.Infrastructure
{
    public class RedisService: IRedisService   
    {
        private readonly IDatabase _database;
        private readonly string _collectionKeyPrefix = "collection:";

        public RedisService(IConfiguration configuration)
        {
            string redisConnectionString = configuration["Redis:ConnectionString"];
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            _database = redis.GetDatabase();
        }

        public async Task<string> GetAsync(string collection, string key)
        {
            var fullKey = GetFullKey(collection, key);
            return await _database.StringGetAsync(fullKey);
        }

        public async Task SetAsync(string collection, string key, string value)
        {
            var fullKey = GetFullKey(collection, key);
            await _database.StringSetAsync(fullKey, value);
        }

        public async Task RemoveAsync(string collection, string key)
        {
            var fullKey = GetFullKey(collection, key);
            await _database.KeyDeleteAsync(fullKey);
        }

        public async Task<IEnumerable<string>> GetAllAsync(string collection)
        {
            var collectionKeys = await GetCollectionKeysAsync(collection);
            var values = new List<string>();

            foreach (var key in collectionKeys)
            {
                values.Add(await _database.StringGetAsync(key));
            }

            return values;
        }

        private string GetFullKey(string collection, string key)
        {
            return $"{_collectionKeyPrefix}{collection}:{key}";
        }

        private async Task<IEnumerable<RedisKey>> GetCollectionKeysAsync(string collection)
        {
            var pattern = $"{_collectionKeyPrefix}{collection}:*";
            var keys = await _database.ExecuteAsync("KEYS", pattern);

            if (keys.IsNull)
            {
                return Enumerable.Empty<RedisKey>();
            }

            return ((string[])keys).Select(key => (RedisKey)key);
        }
    }
}
