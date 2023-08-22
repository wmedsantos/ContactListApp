using System;

namespace ContactList.Domain
{
    public class Contact
    {
        public ContactTypeEnum Type { get; set; }
        public string ContactValue { get; set; }
        public Contact()
        {
            Type =ContactTypeEnum.Phone;
            ContactValue = string.Empty;
        }
        public Contact(ContactTypeEnum type, string contactValue)
        {
            Type = type;
            ContactValue = contactValue;
        }
    }
}
