using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace MainApp_WPF.ViewModels;

public partial class AddContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    : ObservableObject
{
    [ObservableProperty]
    private string _title = "Add New Contact";

    [ObservableProperty]
    private ContactForm _contact = new();

    [RelayCommand]
    private void SaveContact()
    {
        if (contactService.AddContact(Contact))
        {
            var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = serviceProvider.GetRequiredService<ListViewModel>();
        }
        else
        {
            Debug.WriteLine("The contact could not be created.");
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var mainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = serviceProvider.GetRequiredService<ListViewModel>();
    }
}
