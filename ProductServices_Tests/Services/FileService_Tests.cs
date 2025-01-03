using Business.Helpers;
using Business.Models;
using Business.Services;
using Moq;
using Newtonsoft.Json;

namespace Business.Tests.Services;

public class FileService_Tests
{
    [Fact]
    public void AddListToFile_ShouldReturnTrue_WhenListIsAddedToFile()
    {
        // Arrange
        var directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var fileName = "List.json";
        var fileService = new FileService(directoryPath, fileName);

        var list = new List<Contact>();

        // Act
        var result = fileService.AddListToFile(list);

        // Assert
        Assert.True(result);
        Assert.True(File.Exists(Path.Combine(directoryPath, fileName)));

        // Clean
        File.Delete(Path.Combine(directoryPath, fileName));
        Directory.Delete(Path.Combine(directoryPath), true);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnListWithCorrectData_WhenListIsLoadedFromFile()
    {
        // Arrange
        var directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        var fileName = "List.json";
        Directory.CreateDirectory(directoryPath);

        var filePath = Path.Combine(directoryPath, fileName);
        var contacts = new List<Contact>();

        Contact contact = new() { Id = "1", FirstName = "Test1", LastName = "Testsson1", Email = "test1@domain.com", PhoneNumber = 0721234567, StreetAddress = "Testvägen 1", PostalCode = 12345, City = "Test1" };
        contacts.Add(contact);

        var json = JsonConvert.SerializeObject(contacts);
        File.WriteAllText(filePath, json);

        var fileService = new FileService(directoryPath, fileName);

        // Act
        var result = fileService.LoadListFromFile();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Contact>>(result);
        result.Any(x => 
            x.Id == contact.Id && x.FirstName == contact.FirstName && x.LastName == contact.LastName && x.Email == contact.Email && x.PhoneNumber == contact.PhoneNumber && x.StreetAddress == contact.StreetAddress && x.PostalCode == contact.PostalCode && x.City == contact.City);
    }
}
