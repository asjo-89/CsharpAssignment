using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MainApp_WPF.ViewModels;

public partial class UpdateContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    public UpdateContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
    }

    [ObservableProperty]
    private string _contactId;

    [ObservableProperty]
    public string _title = "Update Contact";

    [ObservableProperty]
    public ContactForm contact = new();

    [RelayCommand]
    public void SaveUpdatedContact()
    {
        if (_contactService.UpdateContact(ContactId, Contact)) 
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
        }
        else
        {
            Debug.WriteLine("Contact could not be updated");
        }
    }

    [RelayCommand]
    public void Cancel()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ListViewModel>();
    }

    partial void OnContactIdChanged(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            LoadContactToUpdate(value);
        }
    }

    public void LoadContactToUpdate(string id)
    {
        Contact contactToUpdate = _contactService.GetContactById(id);

        if (contactToUpdate != null)
        {
            Contact = new ContactForm
            {
                FirstName = contactToUpdate.FirstName,
                LastName = contactToUpdate.LastName,
                Email = contactToUpdate.Email,
                PhoneNumber = contactToUpdate.PhoneNumber,
                StreetAddress = contactToUpdate.StreetAddress,
                PostalCode = contactToUpdate.PostalCode,
                City = contactToUpdate.City,
            };
        }
    }
}
