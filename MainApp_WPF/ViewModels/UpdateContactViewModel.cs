using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MainApp_WPF.ViewModels;

public partial class UpdateContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    : ObservableObject
{
    [ObservableProperty]
    private string _contactId = null!;

    [ObservableProperty]
    private string _title = "Update Contact";

    [ObservableProperty] 
    private ContactForm _contact = new();

    [RelayCommand]
    private void SaveUpdatedContact()
    {
        if (contactService.UpdateContact(ContactId, Contact)) 
        {
            var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = serviceProvider.GetRequiredService<ListViewModel>();
        }
        else
        {
            Debug.WriteLine("Contact could not be updated");
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = serviceProvider.GetRequiredService<ListViewModel>();
    }

    partial void OnContactIdChanged(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            LoadContactToUpdate(value);
        }
    }

    private void LoadContactToUpdate(string id)
    {
        Contact? contactToUpdate = contactService.GetContactById(id);

        if (contactToUpdate != null!)
        {
            Contact = new ContactForm
            {
                FirstName = contactToUpdate.FirstName,
                LastName = contactToUpdate.LastName,
                Email = contactToUpdate.Email,
                PhoneNumber = contactToUpdate.PhoneNumber,
                StreetAddress = contactToUpdate.StreetAddress,
                PostalCode = contactToUpdate.PostalCode,
                City = contactToUpdate.City
            };
        }
    }
}
