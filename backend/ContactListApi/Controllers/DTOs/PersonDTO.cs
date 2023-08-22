namespace ContactListApi.Controllers;

    public class PersonDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public int? Age { get; set; }
        public List<ContactDto>? Contacts { get; set; }
    }

    public class ContactDto
    {
        public string? Type { get; set; }
        public string? ContactValue { get; set; }
    }
