using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Services;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;


    public FileService()
    {
        _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Lists");
        _filePath = Path.Combine(_directoryPath, "ContactsList.json");

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
            var json = JsonListConverter.ConvertToJson(list);
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
            var contacts = JsonListConverter.ConvertToList(json);
            return contacts;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message); 
            return [];
        }
    }
}
 