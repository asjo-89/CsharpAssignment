using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using Newtonsoft.Json;

namespace Business.Tests.Services;

public class FileService_Tests
{
    private readonly IJsonConverter _jsonConverter = new JsonListConverter();
    private readonly Mock<IFileService> _mockFileService;
    private readonly Mock<IFileSetupService> _mockFileSetupService;    
    private readonly Mock<IJsonConverter> _mockConverter;

    public FileService_Tests()
    { 
        _mockFileService = new Mock<IFileService>();
        _mockFileService.Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>())).Returns(true);
        _mockFileService.Setup(fs => fs.LoadListFromFile()).Returns([]);
        
        _mockConverter = new Mock<IJsonConverter>();
        _mockConverter.Setup(c => c.ConvertToJson(It.IsAny<List<Contact>>())).Returns("[]");

        _mockFileSetupService = new Mock<IFileSetupService>();
        _mockFileSetupService.Setup(fss => fss.WriteAllText(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("ExceptionTest"));
    }

    

    [Fact]
    public void AddListToFile_ShouldReturnFalse_WhenWriteAllTextThrowsException()
    {
        // Arrange
        string testFilePath = "testFilePath";

        var fileService = new FileService
        (
            _mockConverter.Object,
            _mockFileSetupService.Object,
            testFilePath
        );

        List<Contact> testList = new List<Contact>();
        // Act
        bool result = fileService.AddListToFile(testList);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnListWithCorrectData_WhenListIsLoadedFromFile()
    {
        // Arrange
        string directoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        const string fileName = "List.json";
        string fullPath = Path.Combine(directoryPath, fileName);
        List<Contact> contacts = new();

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
        File.WriteAllText(fullPath, json);
        FileService fileService = new FileService(_jsonConverter, fullPath);

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
