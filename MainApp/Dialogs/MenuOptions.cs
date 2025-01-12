using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace MainApp_Console.Dialogs
{
    public class MenuOptions(IContactService contactService)
    {
        public void CreateContact()
        {
            ContactForm form = ContactFactory.Create();

            Console.Clear();
            Console.Write("Enter first name: ");
            form.FirstName = Console.ReadLine()!.Trim();

            Console.Write("Enter last name: ");
            form.LastName = Console.ReadLine()!.Trim();

            Console.Write("Enter email: ");
            form.Email = Console.ReadLine()!.Trim();

            Console.Write("Enter phone number: ");
            form.PhoneNumber = Console.ReadLine()!.Trim();

            Console.Write("Enter street: ");
            form.StreetAddress = Console.ReadLine()!.Trim();

            bool valid = true;
            while (valid)
            {
                Console.Write("Enter postal code: ");
                int.TryParse(Console.ReadLine()!.Trim(), out int value);
                if (value >= 10000)
                {
                    form.PostalCode = value;
                    valid = false;
                }
                else
                {
                    Console.WriteLine("You must enter 5 digits.\n");
                }
            } 

            Console.Write("Enter city: ");
            form.City = Console.ReadLine()!.Trim();

            contactService.AddToList(form);

            Console.ReadKey();
        }

        public void UpdateContact()
        {
            ContactForm form = new();

            Console.Clear();
            List<Contact> list = contactService.GetContacts().ToList();

            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id[..4]}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
                Console.WriteLine();
            }

            bool valid = true;
            while (valid)
            {
                Console.Write("\nEnter the contact you want to update (1, 2, 3 eg): ");
                bool parsed = int.TryParse(Console.ReadLine()!.Trim(), out int index);
                if (parsed && index <= list.Count && index >= 1)
                {
                    Contact validId = contactService.GetContactById(list[index - 1].Id);

                    if (validId == null!)
                    {
                        Console.WriteLine("No contact with that id was found.");
                        Console.ReadKey();
                        return;
                    }

                    Console.Clear();
                    Console.WriteLine("If there is a field you don't want to change just leave it empty.\n");

                    Console.Write("Enter new first name: ");
                    form.FirstName = Console.ReadLine()!.Trim();

                    Console.Write("Enter new last name: ");
                    form.LastName = Console.ReadLine()!.Trim();

                    Console.Write("Enter new email: ");
                    form.Email = Console.ReadLine()!.Trim();

                    Console.Write("Enter new phone number: ");
                    form.PhoneNumber = Console.ReadLine()!.Trim();

                    Console.Write("Enter new street address: ");
                    form.StreetAddress = Console.ReadLine()!.Trim();

                    while (valid)
                    {
                        Console.Write("Enter new postal code: ");
                        string input = Console.ReadLine()!.Trim();
                        parsed = int.TryParse(input, out int value);
                        
                        if (input == "")
                        {
                            valid = false;
                        }
                        else if (value >= 10000 && parsed)
                        {
                            form.PostalCode = value;
                            valid = false;
                        }
                        else
                        {
                            Console.WriteLine("You must enter 5 digits.\n");
                        }
                    }

                    Console.Write("Enter new city: ");
                    form.City = Console.ReadLine()!;

                    var successUpdate = contactService.UpdateContact(validId.Id, form);

                    if (!successUpdate)
                    {
                        Console.Clear();
                        Console.WriteLine("Something went wrong when updating the contact. Try again later.");
                        Console.ReadKey();
                        return;
                    }

                    valid = false;
                }
                else
                {
                    Console.WriteLine("Enter a digit based on the contact placement in the list.");
                }
            }            

            Console.Clear();
            Console.WriteLine("The contact was successfully updated.");
            Console.ReadKey();
        }

        public void DeleteContact()
        {
            Console.Clear();
            List<Contact> list = contactService.GetContacts().ToList();
            
            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id[..4]}");
                Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email}");
                Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                Console.WriteLine($"Address: {contact.StreetAddress}, {contact.PostalCode} {contact.City}");
                Console.WriteLine();
            }

            bool valid = true;
            while (valid)
            {
                Console.Write("\nEnter the contact you want to delete (1, 2, 3 eg): ");
                string input = Console.ReadLine()!;
                bool parsed = int.TryParse(input, out int index);
                if (parsed && index <= list.Count && index >= 1)
                {
                    Contact validId = contactService.GetContactById(list[index - 1].Id);

                    if (validId == null!)
                    {
                        Console.WriteLine("No contact with that id was found.\n");
                        Console.ReadKey();
                        return;
                    }

                    contactService.DeleteContact(input);
                    Console.WriteLine("The contact was successfully deleted.");
                    Console.ReadKey();
                    valid = false;
                }
                else
                {
                    Console.WriteLine("Enter a digit based on the contact placement in the list.");
                }
            }
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            List<Contact> list = contactService.GetContacts().ToList();

            if (list.Count == 0 || list == null!)
            {
                Console.WriteLine("There are no contacts in the list yet.");
                Console.ReadKey();
                return;
            }

            int counter = 1;
            foreach (Contact contact in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {counter++}:");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine($"Id: {contact.Id[..4]}");
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
