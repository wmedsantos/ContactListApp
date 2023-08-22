using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ContactList.Domain;
using ContactList.Application;
using ContactList.Infrastructure;

namespace ContactListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonAppService _personAppService;
    private readonly IMapper _mapper;

    public PersonController(IPersonAppService personAppService, IMapper mapper)
    {
        _personAppService = personAppService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonDto request)
    {
        
        var person = _mapper.Map<Person>(request);
        
        var personId = await _personAppService.CreatePersonAsync(person);
        
        var responseDto = _mapper.Map<PersonDto>(person);

        return Ok(responseDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPerson(Guid id)
    {

        var person = await _personAppService.GetPersonAsync(id);

        if (person == null)
        {
            return NotFound(); // Person not found
        }

        var personDtoResponse = _mapper.Map<PersonDto>(person);
        return Ok(personDtoResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {

        var persons = await _personAppService.GetAllPersonsAsync();

        var personsDtoResponse = _mapper.Map<IEnumerable<PersonDto>>(persons);

        return Ok(personsDtoResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(Guid id, [FromBody] PersonDto request)
    {
        if (request == null)
        {
            return BadRequest("Invalid JSON data");
        }

        var person = await _personAppService.GetPersonAsync(id);

        if (person == null)
        {
            return NotFound(); // Person not found
        }

        _mapper.Map(request, person);

        await _personAppService.UpdatePersonAsync(person);

        return NoContent(); // Updated successfully
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var person = await _personAppService.GetPersonAsync(id);

        if (person == null)
        {
            return NotFound(); // Person not found
        }

        await _personAppService.DeletePersonAsync(id);

        return NoContent(); // Deleted successfully
    }
}
