﻿using Business.Interfaces;
using Business.Services;
using MainApp_WPF.ViewModels;
using MainApp_WPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MainApp_WPF;
public partial class App
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddTransient<IFileService, FileService>();
                services.AddTransient<IContactService, ContactService>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();

                services.AddTransient<ListViewModel>();
                services.AddTransient<ListView>();

                services.AddTransient<AddContactViewModel>();
                services.AddTransient<AddContactView>();

                services.AddTransient<UpdateContactViewModel>();
                services.AddTransient<UpdateContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _host.Services.GetRequiredService<ListViewModel>();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
