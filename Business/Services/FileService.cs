using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _filePath; 
    private readonly IJsonConverter _jsonConverter;
    private readonly IFileSetupService _fileSetupService;
    
    public FileService(IJsonConverter jsonConverter, IFileSetupService fileSetupService,  string? filePath)
    {
        _jsonConverter = jsonConverter;
        _fileSetupService = fileSetupService;
        _filePath = filePath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lists", "ContactsList.json");
        
        var directoryPath = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directoryPath) && !_fileSetupService.DirectoryExists(directoryPath))
        {
            _fileSetupService.CreateDirectory(directoryPath);
        }
        
        if (!_fileSetupService.FileExists(_filePath))
        {
            _fileSetupService.WriteAllText(_filePath, "[]");
        }
    }

    public bool AddListToFile(List<Contact> list)
    {
        try
        {
            if (list != null!)
            {
                var json = _jsonConverter.ConvertToJson(list);
                _fileSetupService.WriteAllText(_filePath, json);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> LoadListFromFile()
    {
        try
        {
            string json = _fileSetupService.ReadAllText(_filePath);
            List<Contact> contacts = _jsonConverter.ConvertToList(json);
            
            if(contacts != null!) return contacts;

            return [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message); 
            return [];
        }
    }
}
 