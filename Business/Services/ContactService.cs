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


    //public Contact GetContactById(string id)
    //{
    //    Contact? contact = _contacts.FirstOrDefault(x => x.Id.Substring(0, 4) == id);

    //    if (contact != null)
    //    {
    //        return contact;
    //    }

    //    return null!;
    //}

    public Contact GetContactById(string id)
    {
        Contact? contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact == null)
        {
            return null!;
        }
        return contact;
    }



    public bool UpdateContact(string id, ContactForm form)
    {
        if (id == null || form == null) return false;

        for (int i = 0; i < _contacts.Count; i++)
        {
            if (_contacts[i].Id == id)
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
        Contact? contact = _contacts.FirstOrDefault(x => x.Id == id);

        if (contact == null)
        {
            return false;
        }

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
