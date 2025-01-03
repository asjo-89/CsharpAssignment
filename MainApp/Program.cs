
using Business.Dialogs;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string directoryPath = "C:\\WIN24\\VS_Code\\cSharp\\Inlämningar\\CsharpAssignment\\MainApp\\bin\\Debug\\net9.0\\Lists";
string fileName = "ContactList.json";

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<ContactService>();
        services.AddSingleton<IFileService>(provider => new FileService(directoryPath, fileName));
        services.AddSingleton<MainMenu>();
    })
    .UseDefaultServiceProvider(options =>
        options.ValidateScopes = true)
    .Build();

var fileService = host.Services.GetRequiredService<IFileService>();
var menu = host.Services.GetRequiredService<MainMenu>();

fileService.LoadListFromFile();
menu.Menu();