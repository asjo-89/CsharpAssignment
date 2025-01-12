using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class ContactService(IFileService fileService) : IContactService
{
    private readonly List<Contact> _list = fileService.ExtractListFromFile();

    public bool AddToList(ContactForm contactForm)
    {
        if (_list != null!)
        {
            Contact contact = ContactFactory.Create(contactForm);
            _list.Add(contact);
            fileService.AddListToFile(_list);
            return true;
        }

        return false;
    }

    public bool UpdateContact(string id, ContactForm contactForm)
    {
        if (id == null! || contactForm == null!) return false;

        Contact contact = GetContactById(id);

        if (!string.IsNullOrWhiteSpace(contactForm.FirstName)) contact.FirstName = contactForm.FirstName;
        
        if (!string.IsNullOrWhiteSpace(contactForm.LastName)) contact.LastName = contactForm.LastName;
        
        if (!string.IsNullOrWhiteSpace(contactForm.Email)) contact.Email = contactForm.Email;
        
        if (!string.IsNullOrWhiteSpace(contactForm.PhoneNumber)) contact.PhoneNumber = contactForm.PhoneNumber;
        
        if (!string.IsNullOrWhiteSpace(contactForm.StreetAddress)) contact.StreetAddress = contactForm.StreetAddress;
        
        if (contactForm.PostalCode >= 10000) contact.PostalCode = contactForm.PostalCode;
        
        if (!string.IsNullOrWhiteSpace(contactForm.City)) contact.City = contactForm.City;

        fileService.AddListToFile(_list);
        
        return true;
    }

    public bool DeleteContact(string id)
    {
        if (id == null! || id == string.Empty) return false;

        Contact contact = _list.FirstOrDefault(x => x.Id == id) ?? null!;

        if (contact == null!) return false;

        _list.Remove(contact);
        fileService.AddListToFile(_list);
        return true;
    }

    public Contact GetContactById(string id)
    {
        Contact contact = _list.FirstOrDefault(x => x.Id == id) ?? null!;
        return contact;
    }

    public IEnumerable<Contact> GetContacts()
    {
        return fileService.ExtractListFromFile();
    }
    
}