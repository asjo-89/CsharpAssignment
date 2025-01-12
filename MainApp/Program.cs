using MainApp_Console.Dialogs;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<MainMenu>();
    })
    .Build();

var fileService = host.Services.GetRequiredService<IFileService>();
var menu = host.Services.GetRequiredService<MainMenu>();

fileService.ExtractListFromFile();
menu.Menu();