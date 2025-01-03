using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

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
}
