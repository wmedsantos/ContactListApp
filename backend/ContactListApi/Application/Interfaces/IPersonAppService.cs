using System;
using System.Threading.Tasks;
using ContactList.Domain; 

namespace ContactList.Application
{
    public interface IPersonAppService
    {
        Task<string> CreatePersonAsync(Person person);
        Task<Person> GetPersonAsync(Guid id);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<bool> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(Guid id);
    }
}
