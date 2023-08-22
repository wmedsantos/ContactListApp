using ContactList.Domain;
using System.Linq.Expressions;

namespace ContactList.Infrastructure
{
    public interface IPersonRepository
    {
        Task CreateAsync(Person person);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(Guid? id);

        Task<IEnumerable<Person>> GetByConditionAsync(Expression<Func<Person, bool>> condition);

        Task UpdateAsync(Guid? id, Person updatedPerson);

        Task DeleteAsync(Guid? id);
    }
}