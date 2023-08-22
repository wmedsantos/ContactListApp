using System;
using System.Threading.Tasks;
using ContactList.Domain; 
using ContactList.Infrastructure;

namespace ContactList.Application
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IPersonRepository _personRepository;

        public PersonAppService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<string> CreatePersonAsync(Person person)
        {
            await _personRepository.CreateAsync(person);
            
            return person.Id.ToString();
        }

        public async Task<Person> GetPersonAsync(Guid id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            var existingPerson = await _personRepository.GetByIdAsync(person.Id);

            if (existingPerson == null)
            {
                return false; // Person not found
            }

            // Update the person's properties
            existingPerson.Name = person.Name;
            existingPerson.Sex = person.Sex;
            existingPerson.Age = person.Age;
            existingPerson.Contacts = person.Contacts;

            await _personRepository.UpdateAsync(person.Id, existingPerson);

            return true;
        }

        public async Task<bool> DeletePersonAsync(Guid id)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id);

            if (existingPerson == null)
            {
                return false; // Person not found
            }

            await _personRepository.DeleteAsync(id);

            return true;
        }
    }
}
