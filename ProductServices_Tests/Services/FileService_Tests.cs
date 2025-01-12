using Business.Models;
using Business.Services;

namespace Business.Tests.Services;

public class FileService_Tests
{

    [Fact]
    public void AddListToFile_ShouldReturnTrue_WhenListIsAddedToFile()
    {
        // Arrange
        var fileService = new FileService("TestFile.json", "TestDirectory");
        List<Contact> testList = [];
        
        // Act
        bool result = fileService.AddListToFile(testList);
        
        // Assert
        Assert.True(result);

        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestDirectory");
        string filePath = Path.Combine(directoryPath, "TestFile.json");

        Assert.True(File.Exists(filePath));

        //Clean
        File.Delete(filePath);
        Directory.Delete(directoryPath);
    }

    [Fact]
    public void ExtractListFromFile_ShouldReturnListWithContentFromJsonFile_WhenListIsLoadedFromFile()
    {
        // Arrange
        var fileService = new FileService("TestFile.json", "TestDirectory");

        Contact testContact = new()
        {
            Id = "1",
            FirstName = "Test",
            LastName = "Test",
            Email = "test@test.com",
            PhoneNumber = "12345",
            StreetAddress = "Main Street",
            PostalCode = 12345,
            City = "Test"
        };

        List<Contact> testList = [];
        testList.Add(testContact);
        fileService.AddListToFile(testList);


        // Act
        var result = fileService.ExtractListFromFile();

        // Assert
        Assert.IsType<List<Contact>>(result);
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal(testContact.Id, result[0].Id);
        Assert.Equal(testContact.FirstName, result[0].FirstName);
        Assert.Equal(testContact.LastName, result[0].LastName);
        Assert.Equal(testContact.Email, result[0].Email);
        Assert.Equal(testContact.PhoneNumber, result[0].PhoneNumber);
        Assert.Equal(testContact.StreetAddress, result[0].StreetAddress);
        Assert.Equal(testContact.PostalCode, result[0].PostalCode);
        Assert.Equal(testContact.City, result[0].City);

        //Clean
        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestDirectory");
        string filePath = Path.Combine(directoryPath, "TestFile.json");
        File.Delete(filePath);
        Directory.Delete(directoryPath);
    }

    [Fact]
    public void ExtractListFromFile_ShouldReturnEmptyList_WhenJsonIsInvalid()
    {
        // Arrange
        var fileService = new FileService("TestFile.json", "TestDirectory");
        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TestDirectory");
        string filePath = Path.Combine(directoryPath, "TestFile.json");

        File.WriteAllText(filePath, "Invalid JSON");
        
        // Act
        var result = fileService.ExtractListFromFile();

        // Assert
        Assert.Empty(result);

        ////Clean
        File.Delete(filePath);
        Directory.Delete(directoryPath);
    }
}
