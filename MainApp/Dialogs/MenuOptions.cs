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
            form.FirstName = Console.ReadLine()!;

            Console.Write("Enter last name: ");
            form.LastName = Console.ReadLine()!;

            Console.Write("Enter email: ");
            form.Email = Console.ReadLine()!;

            Console.Write("Enter phone number: ");
            form.PhoneNumber = Console.ReadLine()!;

            Console.Write("Enter street: ");
            form.StreetAddress = Console.ReadLine()!;

            bool valid = true;
            while (valid)
            {
                Console.Write("Enter postal code: ");
                bool parsed = int.TryParse(Console.ReadLine(), out int value);
                if (parsed)
                {
                    form.PostalCode = value;
                    valid = false;
                }
                else
                {
                    Console.WriteLine("Enter only digits.");
                }
            } 

            Console.Write("Enter city: ");
            form.City = Console.ReadLine()!;

            contactService.AddContact(form);

            Console.ReadKey();
        }

        public void UpdateContact()
        {
            ContactForm form = new();

            Console.Clear();
            List<Contact> list = contactService.GetAll().ToList();

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
                bool parsed = int.TryParse(Console.ReadLine(), out int index);
                if (parsed && index <= list.Count && index >= 1)
                {
                    Contact? validId = contactService.GetContactById(list[index - 1].Id);

                    if (validId == null)
                    {
                        Console.WriteLine("No contact with that id was found.");
                        Console.ReadKey();
                        return;
                    }

                    Console.Clear();
                    Console.WriteLine("If there is a field you don't want to change just leave it empty.\n");

                    Console.Write("Enter new first name: ");
                    form.FirstName = Console.ReadLine()!;

                    Console.Write("Enter new last name: ");
                    form.LastName = Console.ReadLine()!;

                    Console.Write("Enter new email: ");
                    form.Email = Console.ReadLine()!;

                    Console.Write("Enter new phone number: ");
                    form.PhoneNumber = Console.ReadLine()!;

                    Console.Write("Enter new street address: ");
                    form.StreetAddress = Console.ReadLine()!;

                    while (valid)
                    {
                        Console.Write("Enter new postal code: ");
                        string input = Console.ReadLine()!;
                        
                        if (input == "")
                        {
                            valid = false;
                        }
                        else if (int.TryParse(input, out int value))
                        {
                            form.PostalCode = value;
                            valid = false;
                        }
                        else
                        {
                            Console.WriteLine("Enter only digits.");
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
            List<Contact> list = contactService.GetAll().ToList();

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
                bool parsed = int.TryParse(Console.ReadLine(), out int index);
                if (parsed)
                {
                    //Contact validId = _contactService.GetContactById(list[index - 1].Id);
                    var delete = contactService.DeleteContact(list[index - 1].Id);
                    switch (delete)
                    {
                        case true:
                            Console.WriteLine("The contact was successfully deleted.");
                            Console.ReadKey();
                            valid = false;
                            break;
                        case false:
                            Console.WriteLine("No contact with that id was found.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter only digits.");
                }
            }
        }

        public void ShowAllContacts()
        {
            Console.Clear();
            var list = contactService.GetAll();

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
