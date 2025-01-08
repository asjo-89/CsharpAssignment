using Business.Converters;
using Business.Models;
using Business.Services;
using Newtonsoft.Json;

namespace Business.Tests.Services;

public class FileService_Tests
{
    private readonly DefaultJsonConverter _jsonConverter = new();
    
    [Fact]
    public void AddListToFile_ShouldReturnTrue_WhenListIsAddedToFile()
    {
        // Arrange
        string directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        const string fileName = "List.json";
        string fullPath = Path.Combine(directoryPath, fileName);
        Directory.CreateDirectory(directoryPath);
        FileService fileService = new FileService(_jsonConverter, fullPath);
        List<Contact> list = new List<Contact>();

        // Act
        bool result = fileService.AddListToFile(list);

        // Assert
        Assert.True(result);
        Assert.True(File.Exists(fullPath));

        // Clean
        File.Delete(Path.Combine(fullPath));
        Directory.Delete(Path.Combine(directoryPath), true);
    }
    
    //Separat test för om filen skapas.

    [Fact]
    public void LoadListFromFile_ShouldReturnListWithCorrectData_WhenListIsLoadedFromFile()
    {
        // Arrange
        string directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        const string fileName = "List.json";
        Directory.CreateDirectory(directoryPath);

        string filePath = Path.Combine(directoryPath, fileName);
        List<Contact> contacts = new List<Contact>();

        Contact contact = new()
        {
            Id = "1", 
            FirstName = "Test1", 
            LastName = "Testsson1", 
            Email = "test1@domain.com", 
            PhoneNumber = "0721234567", 
            StreetAddress = "Testvägen 1", 
            PostalCode = 12345, 
            City = "Test1"
        };
        contacts.Add(contact);

        string json = JsonConvert.SerializeObject(contacts);
        File.WriteAllText(filePath, json);

        FileService fileService = new FileService(_jsonConverter);

        // Act
        List<Contact> result = fileService.LoadListFromFile();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Contact>>(result);
        _= result.Any(x =>
            x.Id == contact.Id 
            && x.FirstName == contact.FirstName 
            && x.LastName == contact.LastName 
            && x.Email == contact.Email 
            && x.PhoneNumber == contact.PhoneNumber 
            && x.StreetAddress == contact.StreetAddress 
            && x.PostalCode == contact.PostalCode 
            && x.City == contact.City);

        // Clean
        File.Delete(Path.Combine(directoryPath, fileName));
        Directory.Delete(Path.Combine(directoryPath), true);
    }
}
