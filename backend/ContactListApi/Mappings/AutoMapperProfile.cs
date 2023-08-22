using AutoMapper;
using ContactList.Domain; 
using ContactList.Application; 
using ContactListApi.Controllers;

namespace ContactList.Mappings;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
         //CreateMap<string, Guid?>().ConvertUsing(s => String.IsNullOrWhiteSpace(s) ? (Guid?)null : Guid.Parse(s));
         //CreateMap<Guid?, string>().ConvertUsing(g => g.HasValue ? g.Value.ToString("N") : null);
        // CreateMap<Guid, string>().ConvertUsing(g => g.ToString("N"));
        CreateMap<PersonDto, Person>()
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => MapContacts(src.Contacts)));
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => MapContactsDto(src.Contacts)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value.ToString("D")));
                    
    }

    private List<Contact> MapContacts(List<ContactDto> contacts)
    {
        var mappedContacts = new List<Contact>();

        foreach (var contactRequest in contacts)
        {
            if (Enum.TryParse<ContactTypeEnum>(contactRequest.Type, out var contactType))
            {
                var contact = new Contact
                {
                    Type = contactType,
                    ContactValue = contactRequest.ContactValue
                };
                mappedContacts.Add(contact);
            }
        }

        return mappedContacts;
    }

    private List<ContactDto> MapContactsDto(List<Contact> contacts)
    {
        var mappedContacts = new List<ContactDto>();

        foreach (var contact in contacts)
        {
            var contactDto = new ContactDto
            {
                Type = contact.Type.ToString(), 
                ContactValue = contact.ContactValue
            };
            mappedContacts.Add(contactDto);
        }

        return mappedContacts;
    }

}
