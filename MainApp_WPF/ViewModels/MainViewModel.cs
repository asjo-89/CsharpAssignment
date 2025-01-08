using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp_WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        CurrentViewModel = serviceProvider.GetRequiredService<ListViewModel>();
    }
}
