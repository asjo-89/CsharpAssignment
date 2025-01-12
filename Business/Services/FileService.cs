using System.Diagnostics;
using System.Text.Json;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    public FileService()
        : this("Lists", "ContactList.json")
    { }

    public FileService(string fileName, string directoryName)
    {
        _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), directoryName);
        _filePath = Path.Combine(_directoryPath, fileName);

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
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
            JsonSerializerOptions options = new() { WriteIndented = true };

            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(_filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> ExtractListFromFile()
    {
        try
        {
            string json = File.ReadAllText(_filePath);
            List<Contact>? contacts = JsonSerializer.Deserialize<List<Contact>>(json);

            if (contacts is null) return [];

            return contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}