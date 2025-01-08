using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _filePath; 
    private readonly IJsonConverter _jsonConverter;

    // Kolla konstruktorn
    public FileService(IJsonConverter jsonConverter) 
        : this(jsonConverter, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lists", "ContactsList.json"))
    {}
    
    public FileService(IJsonConverter jsonConverter, string filePath)
    {
        _jsonConverter = jsonConverter;
        _filePath = filePath;
        
        var directoryPath = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directoryPath) && !string.IsNullOrWhiteSpace(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public bool AddListToFile(List<Contact> list)
    {
        try
        {
            var json = _jsonConverter.ConvertToJson(list);
            File.WriteAllText(_filePath, json);
            return true;
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
            string json = File.ReadAllText(_filePath);
            var contacts = _jsonConverter.ConvertToList(json);
            return contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message); 
            return [];
        }
    }
}
 