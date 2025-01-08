using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private readonly List<Contact> _contacts;


    public ContactService(IFileService fileService)
    {
        _fileService = fileService;
        _contacts = _fileService.LoadListFromFile();
    }

    public bool AddContact(ContactForm form)
    {
        try
        {
            Contact contact = ContactFactory.Create(form);
            _contacts.Add(contact);
            _fileService.AddListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to create user. Please try again.");
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public Contact? GetContactById(string id)
    {
        return _contacts.FirstOrDefault(x => x.Id == id);
    }

    public bool UpdateContact(string? id, ContactForm? form)
    {
        if (id == null || form == null) return false;

        foreach (Contact t in _contacts)
        {
            if (t.Id != id) continue;
            
            if (!string.IsNullOrWhiteSpace(form.FirstName)) t.FirstName = form.FirstName;

            if (!string.IsNullOrWhiteSpace(form.LastName)) t.LastName = form.LastName;

            if (!string.IsNullOrWhiteSpace(form.Email)) t.Email = form.Email;

            int phone = form.PhoneNumber.Length;
            if (phone > 0) t.PhoneNumber = form.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(form.StreetAddress)) t.StreetAddress = form.StreetAddress;

            int postalCode = form.PostalCode.ToString()!.Length;
            if (postalCode >= 5) t.PostalCode = form.PostalCode;

            if (!string.IsNullOrWhiteSpace(form.City)) t.City = form.City;

            _fileService.AddListToFile(_contacts);
            return true;
        }
        return false;
    }

    public bool DeleteContact(string id)
    {
        Contact? contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact == null) return false;

        _contacts.Remove(contact);
        _fileService.AddListToFile(_contacts);
        return true;
    }

    public IEnumerable<Contact> GetAll()
    {
        var newList = _fileService.LoadListFromFile();
        return newList;
    }
}
