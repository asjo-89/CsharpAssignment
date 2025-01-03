using Business.Models;
using System.Diagnostics;

namespace Business.Factories;

public static class ContactFactory
{
    public static ContactForm Create() => new ContactForm();
    public static Contact Create(ContactForm form)
    {
        try
        {
            Contact newContact = new()
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                StreetAddress = form.StreetAddress,
                PostalCode = form.PostalCode,
                City = form.City,
            };
            return newContact;

        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong. Try again.");
            Debug.WriteLine(e.Message);
            return new Contact();
        }
    }
}
