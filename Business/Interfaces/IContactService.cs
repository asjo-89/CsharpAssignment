using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool AddToList(ContactForm contactForm);
    bool UpdateContact(string id, ContactForm contactForm);
    bool DeleteContact(string id);
    Contact GetContactById(string id);
    IEnumerable<Contact> GetContacts();
}