﻿using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace MainApp_WPF.ViewModels;

public partial class ListViewModel : ObservableObject
{
    private readonly IContactService _contactService;
    private readonly IServiceProvider _serviceProvider;

    public ListViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        LoadContacts();
    }

    [ObservableProperty]
    private string _title = "Contacts";

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    [RelayCommand]
    private void GoToAddContactView()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    private void GoToUpdateContactView(Contact contact)
    {
        var viewModel = _serviceProvider.GetRequiredService<UpdateContactViewModel>();
        viewModel.ContactId = contact.Id;
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = viewModel;
    }

    [RelayCommand]
    private void DeleteContact(Contact? contact)
    {
        if (contact is null)
        {
            return;
        }

        _contactService.DeleteContact(contact.Id);
        Contacts.Remove(contact);
    }

    private void LoadContacts()
    {
        IEnumerable<Contact> contacts = _contactService.GetContacts();

        foreach (Contact contact in contacts)
        {
            Contacts.Add(contact);
        }
    }
}
