using Business.Helpers;
using MainApp_Console.Dialogs;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddTransient<IJsonConverter, JsonListConverter>();
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<ContactService>();
        services.AddSingleton<IFileSetupService, FileSetupService>();
        services.AddSingleton<IFileService>(provider =>
        {
            var jsonConverter = provider.GetRequiredService<IJsonConverter>();
            var fileSetupService = provider.GetRequiredService<IFileSetupService>();
            return new FileService(jsonConverter, fileSetupService, null);
        });
        services.AddSingleton<MainMenu>();
    })
    .UseDefaultServiceProvider(options =>
        options.ValidateScopes = true)
    .Build();

var fileService = host.Services.GetRequiredService<IFileService>();
var menu = host.Services.GetRequiredService<MainMenu>();

fileService.LoadListFromFile();
menu.Menu();