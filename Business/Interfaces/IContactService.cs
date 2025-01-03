using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    IEnumerable<Contact> GetAll();
    bool FindContactById(string id);
    bool UpdateContact(string id, ContactForm form);
    bool DeleteContact(string id);
    bool AddContact(ContactForm form);
}
