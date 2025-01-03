using Business.Factories;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService : IContactService
{
    private readonly IFileService _fileService;
    private readonly List<Contact> _contacts = [];


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

    //Sök efter namn istället

    public bool FindContactById(string id)
    {
        var contact = _contacts.FirstOrDefault(x => x.Id.Substring(0, 4) == id);

        if (contact != null)
        {
            return true;
        }

        return false;
    }

    public bool UpdateContact(string id, ContactForm form)
    {
        for (int i = 0; i < _contacts.Count; i++)
        {
            if (_contacts[i].Id.Substring(0, 4) == id)
            {
                if (!string.IsNullOrWhiteSpace(form.FirstName))
                {
                    _contacts[i].FirstName = form.FirstName;
                }

                if (!string.IsNullOrWhiteSpace(form.LastName))
                {
                    _contacts[i].LastName = form.LastName;
                }

                if (!string.IsNullOrWhiteSpace(form.Email))
                {
                    _contacts[i].Email = form.Email;
                }

                int phone = form.PhoneNumber.ToString().Length;
                if (phone > 0)
                {
                    _contacts[i].PhoneNumber = form.PhoneNumber;
                }

                if (!string.IsNullOrWhiteSpace(form.StreetAddress))
                {
                    _contacts[i].StreetAddress = form.StreetAddress;
                }

                int postalCode = form.PostalCode.ToString().Length;
                if (postalCode >= 5)
                {
                    _contacts[i].PostalCode = form.PostalCode;
                }

                if (!string.IsNullOrWhiteSpace(form.City))
                {
                    _contacts[i].City = form.City;
                }

                _fileService.AddListToFile(_contacts);
                return true;
            }
        }
        return false;
    }

    public bool DeleteContact(string id)
    {
        for (int i = 0; i < _contacts.Count; i++)
        {
            if (_contacts[i].Id.Substring(0, 4) == id)
            {
                _contacts.RemoveAt(i);
                _fileService.AddListToFile(_contacts);
                return true;
            }
        }        
        return false;
    }

    public IEnumerable<Contact> GetAll()
    {
        var newList = _fileService.LoadListFromFile();
        return newList;
    }
}
