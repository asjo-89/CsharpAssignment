using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Business.Dialogs;

public class MainMenu
{
    private readonly IContactService _contactService;
    private readonly IFileService _fileService;
    private readonly MenuOptions _menuOptions;

    public MainMenu(IContactService contactService, IFileService fileservice)
    {
        _contactService = contactService;
        _fileService = fileservice;
        _menuOptions = new MenuOptions(contactService);
    }

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
