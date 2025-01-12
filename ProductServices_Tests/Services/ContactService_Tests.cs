using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService1> _fileServiceMock = new();
    
    private readonly ContactForm _contact1 = new()
    {
        FirstName = "Test2",
        LastName = "Testsson2",
        Email = "test2@domain.com",
        PhoneNumber = "2111111111",
        StreetAddress = "Testvägen 2",
        PostalCode = 22222,
        City = "Teststad"
    };
    private readonly ContactForm _updatedContact1 = new()
    {
        FirstName = "Testing",
        LastName = "Testing",
        Email = "testing@domain.com",
        PhoneNumber = "0",
        StreetAddress = "Testvägen 1",
        PostalCode = 11111,
        City = "Teststad1"
    };
    private readonly ContactForm _updatedContact2 = new()
    {
        FirstName = "Testing",
        LastName = "Testing",
        Email = "",
        PhoneNumber = "",
        StreetAddress = "Testvägen 2",
        PostalCode = 22222,
        City = "Teststad"
    };
    private readonly Contact _testContact1 = new()
    {
        Id = "1",
        FirstName = "Test1", LastName = "Testsson1",
        Email = "test1@domain.com", PhoneNumber = "1111111111",
        StreetAddress = "Test", PostalCode = 11111, City = "Teststad"
    };
    private readonly Contact _testContact2 = new()
    {
        Id = "2",
        FirstName = "Test2", LastName = "Testsson2",
        Email = "test2@domain.com", PhoneNumber = "2121212121",
        StreetAddress = "Test", PostalCode = 22222, City = "Teststad"
    };

    [Fact]
    public void AddContact_ShouldReturnTrue_WhenContactIsAddedSuccessfully()
    {
        // Arrange
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns([]);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>()))
            .Returns(true);
        var contactService = new ContactService1(_fileServiceMock.Object);
        
        // Act
        var result = contactService.AddContact(_contact1);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(It.IsAny<List<Contact>>()), Times.Once);
    }

    [Fact]
    public void AddContact_ShouldReturnFalse_WhenListIsNull()
    {
        // Arrange
        List<Contact> testList = null!;
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(false);
        var contactService = new ContactService1(_fileServiceMock.Object);
        
        // Act
        var result = contactService.AddContact(_contact1);

        // Assert
        Assert.False(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Never);
    }

    [Fact]
    public void GetAll_ShouldReturnIEnumerableList()
    {
        // Arrange
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns([]);
        var contactService = new ContactService1(_fileServiceMock.Object);

        // Act
        var result = contactService.GetAll().ToList();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
    }

    [Fact]
    public void GetContactById_ShouldReturnSpecifiedContact_WhenIdMatchesIdInList()
    {
        // Arrange
        List<Contact> testList = [_testContact1, _testContact2];
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        var contactService = new ContactService1(_fileServiceMock.Object);
        
        // Act
        Contact? result = contactService.GetContactById(_testContact2.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        Assert.Equal(result.Id, _testContact2.Id);
        Assert.Equal(result.FirstName, _testContact2.FirstName);
    }
    
    [Fact]
    public void UpdateContact_ShouldReturnTrue_WhenContactIsUpdatedInList_EmptyInputsWillNotUpdate()
    {
        // Arrange
        List<Contact> testList = [_testContact1];
        var id = testList[0].Id;
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(true);
        var contactService = new ContactService1(_fileServiceMock.Object);

        // Act
        bool result = contactService.UpdateContact(id, _updatedContact1);

        //Assert
        Assert.True(result);
        Assert.Single(testList);
        Assert.Equal(testList[0].Id, _testContact1.Id);
        Assert.Equal(_updatedContact1.FirstName, testList[0].FirstName);
        Assert.Equal(_updatedContact1.LastName, testList[0].LastName);
        Assert.Equal(_updatedContact1.Email, testList[0].Email);
        Assert.Equal(_updatedContact1.PhoneNumber, testList[0].PhoneNumber);
        Assert.Equal(_updatedContact1.StreetAddress, testList[0].StreetAddress);
        Assert.Equal(_updatedContact1.PostalCode, testList[0].PostalCode);
        Assert.Equal(_updatedContact1.City, testList[0].City);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Once);
    }

    [Fact]
    public void UpdateContact_ShouldNotUpdateExistingDetails_WhenInputFieldIsEmpty()
    {
        // Arrange
        List<Contact> testList = [_testContact1];
        var id = testList[0].Id;
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(true);
        var contactService = new ContactService1(_fileServiceMock.Object);

        
        // Act
        bool result = contactService.UpdateContact(id, _updatedContact2);
        
        // Assert
        Assert.True(result);
        Assert.Single(testList);
        Assert.Equal(testList[0].Id, _testContact1.Id);
        Assert.Equal(_updatedContact2.FirstName, testList[0].FirstName);
        Assert.Equal(_updatedContact2.LastName, testList[0].LastName);
        Assert.NotEqual(_updatedContact2.Email, testList[0].Email);
        Assert.NotEqual(_updatedContact2.PhoneNumber, testList[0].PhoneNumber);
        Assert.Equal(_updatedContact2.StreetAddress, testList[0].StreetAddress);
        Assert.Equal(_updatedContact2.PostalCode, testList[0].PostalCode);
        Assert.Equal(_updatedContact2.City, testList[0].City);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Once);
    }
    
    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactIsDeletedFromList()
    {
        // Arrange
        List<Contact> testList = [_testContact1];
        var id = testList[0].Id;
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(true);
        var contactService = new ContactService1(_fileServiceMock.Object);

        // Act
        bool result = contactService.DeleteContact(id);

        //Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Once);
    }
}
    
