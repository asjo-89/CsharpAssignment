using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Business.Dialogs
{
    public class MenuOptions(IContactService contactService)
    {
        private readonly IContactService _contactService = contactService;


        public void CreateContact()
        {
            ContactForm form = ContactFactory.Create();

            Console.Clear();
            Console.Write("Enter first name: ");
            form.FirstName = Console.ReadLine()!;

            Console.Write("Enter last name: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("Enter email: ");
            form.Email = Console.ReadLine()!;

            Console.Write("Enter phone number: ");
            int.TryParse(Console.ReadLine(), out int value);
            form.PhoneNumber = value;

            Console.Write("Enter street: ");
            form.StreetAddress = Console.ReadLine()!;

            Console.Write("Enter postal code: ");
            int.TryParse(Console.ReadLine(), out value);
            form.PostalCode = value;

            Console.Write("Enter city: ");
            form.City = Console.ReadLine()!;

            _contactService.AddContact(form);

            Console.ReadKey();
        }

        public void UpdateContact()
        {
            ContactForm form = new();

            Console.Clear();
            List<Contact> list = _contactService.GetAll().ToList();

            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id.Substring(0, 4)}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
                Console.WriteLine();
            }

            Console.Write("\nEnter the contact you want to update (1, 2, 3 eg): ");
            string input = Console.ReadLine()!.Trim();
            int.TryParse(input, out int index);

            Contact validId = _contactService.GetContactById(list[index -1].Id);

            if (validId == null)
            {
                Console.WriteLine("No contact with that id was found.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("\nIf there is a field you don't want to change just leave it empty.\n");

            Console.Write("Enter new first name: ");
            form.FirstName = Console.ReadLine()!;

            Console.Write("\nEnter new last name: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("\nEnter new email: ");
            form.Email = Console.ReadLine()!;

            Console.Write("\nEnter new phone number: ");
            int.TryParse (Console.ReadLine(), out int value);
            form.PhoneNumber = value;

            Console.Write("\nEnter new street address: ");
            form.StreetAddress = Console.ReadLine()!;

            Console.Write("\nEnter new postal code: ");
            int.TryParse(Console.ReadLine(), out value);
            form.PostalCode = value;

            Console.Write("Enter new city: ");
            form.City = Console.ReadLine()!;

            var successUpdate = _contactService.UpdateContact(validId.Id, form);

            if (!successUpdate)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong when updating the contact. Try again later.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("The contact was successfully updated.");

            Console.ReadKey();
        }

        public void DeleteContact()
        {
            ContactForm form = new();

            Console.Clear();
            List<Contact> list = _contactService.GetAll().ToList();

            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id.Substring(0, 4)}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
                Console.WriteLine();
            }

            Console.Write("\nEnter the contact you want to delete (1, 2, 3 eg): ");
            string input = Console.ReadLine()!.Trim();
            int.TryParse(input, out int index);

            Contact validId = _contactService.GetContactById(list[index - 1].Id);

            if (validId == null)
            {
                Console.WriteLine("No contact with that id was found.");
                Console.ReadKey();
                return;
            }
            
            var delete = _contactService.DeleteContact(list[index - 1].Id);
            
            if (!delete)
            {
                Console.WriteLine("No contact with that id was found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The contact was successfully deleted.");
            Console.ReadKey();
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            var list = _contactService.GetAll();

            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor= ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id.Substring(0, 4)}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
