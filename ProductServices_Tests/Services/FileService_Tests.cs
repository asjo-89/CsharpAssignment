using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;
using Newtonsoft.Json;

namespace Business.Tests.Services;

public class FileService_Tests
{
    private readonly Mock<IFileSetupService> _mockFileSetupService = new();    
    private readonly Mock<IJsonConverter> _mockConverter = new();

    [Fact]
    public void AddListToFile_ShouldReturnTrue_WhenListIsAddedToFile()
    {
        // Arrange
        string testFilePath = "testFilePath";
        List<Contact> list = new List<Contact>();
        
        _mockConverter
            .Setup(c => c.ConvertToJson(list))
            .Returns("[]");
        _mockFileSetupService
            .Setup(f => f.FileExists(testFilePath))
            .Returns(true);
        _mockFileSetupService
            .Setup(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

        var fileService = new FileService1(
            _mockConverter.Object,
            _mockFileSetupService.Object,
            testFilePath);
        
        // Act
        bool result = fileService.AddListToFile(list);
        
        // Assert
        Assert.True(result);
        _mockFileSetupService.Verify(fss => fss.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public void AddListToFile_ShouldReturnFalse_WhenWriteAllTextThrowsException()
    {
        // Arrange
        _mockConverter
            .Setup(c => c.ConvertToJson(It.IsAny<List<Contact>>()))
            .Returns("[]");
        _mockFileSetupService
            .Setup(c => c.FileExists(It.IsAny<string>()))
            .Returns(true);
        _mockFileSetupService
            .Setup(fss => fss.WriteAllText(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("ExceptionTest"));

        string testFilePath = "testFilePath";
        var fileService = new FileService1(
            _mockConverter.Object,
            _mockFileSetupService.Object,
            testFilePath);
        List<Contact> testList = new List<Contact>();
        
        // Act
        bool result = fileService.AddListToFile(testList);

        // Assert
        Assert.False(result);
        _mockFileSetupService.Verify(fss => fss.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnListWithContentFromJsonFile_WhenListIsLoadedFromFile()
    {
        // Arrange
        string testFile = "testFile";
        Contact testContact = new Contact
        {
            Id = "1",
            FirstName = "Test", LastName = "Test",
            Email = "test@test.com", PhoneNumber = "12345",
            StreetAddress = "Main Street", PostalCode = 12345, City = "Test"
        };
        
        List<Contact> testList = new();
        testList.Add(testContact);
        string testContent = JsonConvert.SerializeObject(testList);
        
        _mockConverter
            .Setup(c => c.ConvertToList(testContent))
            .Returns(testList);
        _mockFileSetupService
            .Setup(fss => fss.ReadAllText(testFile))
            .Returns(testContent);

        var fileService = new FileService1(
            _mockConverter.Object,
            _mockFileSetupService.Object,
            testFile);

        // Act
        var result = fileService.LoadListFromFile();

        // Assert
        Assert.IsType<List<Contact>>(result);
        Assert.Equal(testList, result);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnEmptyList_WhenJsonFileIsInvalid()
    {
        // Arrange
        string testFile = "testFile";
        string invalidJsonFile = "invalidJsonFile"; 
        
        _mockFileSetupService
            .Setup(fss => fss.ReadAllText(testFile))
            .Returns(invalidJsonFile);
        _mockConverter
            .Setup(c => c.ConvertToList(invalidJsonFile))
            .Returns(new List<Contact>());
        
        var fileService = new FileService1(
            _mockConverter.Object, 
            _mockFileSetupService.Object, 
            testFile
            );
        
        // Act
        var result = fileService.LoadListFromFile();
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockConverter.Verify(c => c.ConvertToList(invalidJsonFile), Times.Once);
    }
}
