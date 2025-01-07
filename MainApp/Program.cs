
using MainApp_Console.Dialogs;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<ContactService>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<MainMenu>();
    })
    .UseDefaultServiceProvider(options =>
        options.ValidateScopes = true)
    .Build();

var fileService = host.Services.GetRequiredService<IFileService>();
var menu = host.Services.GetRequiredService<MainMenu>();

fileService.LoadListFromFile();
menu.Menu();