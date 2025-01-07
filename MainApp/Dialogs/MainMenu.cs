using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace MainApp_Console.Dialogs;

public class MainMenu(IContactService contactService)
{
    private readonly MenuOptions _menuOptions = new(contactService);

    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********** CONTACT LIST **********\n");
            Console.ForegroundColor= ConsoleColor.Gray;

            Console.WriteLine("1. Create new contact");
            Console.WriteLine("2. Show all contacts");
            Console.WriteLine("3. Update contact");
            Console.WriteLine("4. Delete contact");
            Console.WriteLine("Q. Quit\n");

            string menuOption = Console.ReadLine()!;

            switch (menuOption.ToLower())
            {
                case "1":
                    _menuOptions.CreateContact();
                    break;

                case "2":
                    _menuOptions.ShowAllContacts();
                    break;

                case "3":
                    _menuOptions.UpdateContact();
                    break;

                case "4":
                    _menuOptions.DeleteContact();
                    break;

                case "q":
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid menu option. Try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
