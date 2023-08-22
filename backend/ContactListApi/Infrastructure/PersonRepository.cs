using System.Threading.Tasks;
using ContactList.Domain;
using System.Linq.Expressions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ContactList.Infrastructure
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IRedisService _redisService;
        private readonly string _collectionName = "Persons";

        public PersonRepository(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task CreateAsync(Person person)
        {
            if (string.IsNullOrEmpty(person.Id.ToString()))
                person.Id = Guid.NewGuid();
            var json = JsonConvert.SerializeObject(person);
            await _redisService.SetAsync(_collectionName, person.Id.ToString(), json);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var persons = await _redisService.GetAllAsync(_collectionName);
            return persons.Select(json => JsonConvert.DeserializeObject<Person>(json));
        }

        public async Task<Person> GetByIdAsync(Guid? id)
        {
            var json = await _redisService.GetAsync(_collectionName, id.ToString());
            return json != null ? JsonConvert.DeserializeObject<Person>(json) : null;
        }

        public async Task<IEnumerable<Person>> GetByConditionAsync(Expression<Func<Person, bool>> condition)
        {
            // Implement this method based on your specific condition
            // You can query GetAllAsync() and filter the results using LINQ
            var allPersons = await GetAllAsync();
            return allPersons.Where(condition.Compile());
        }

        public async Task UpdateAsync(Guid? id, Person updatedPerson)
        {
            var json = JsonConvert.SerializeObject(updatedPerson);
            await _redisService.SetAsync(_collectionName, id.ToString(), json);
        }

        public async Task DeleteAsync(Guid? id)
        {
            await _redisService.RemoveAsync(_collectionName, id.ToString());
        }
    }

}