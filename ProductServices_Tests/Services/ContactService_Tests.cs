using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    
    private readonly ContactForm _contact1 = new()
    {
        FirstName = "Test1",
        LastName = "Testsson1",
        Email = "test1@domain.com",
        PhoneNumber = "1111111111",
        StreetAddress = "Testvägen 1",
        PostalCode = 11111,
        City = "Teststad"
    };
    private readonly ContactForm _contact2 = new()
    {
        FirstName = "Test2",
        LastName = "Testsson2",
        Email = "test2@domain.com",
        PhoneNumber = "2111111111",
        StreetAddress = "Testvägen 2",
        PostalCode = 22222,
        City = "Teststad"
    };
    private readonly ContactForm _updatedContact = new()
    {
        FirstName = "Testing",
        LastName = "Testing",
        Email = "",
        PhoneNumber = "0",
        StreetAddress = "Testvägen 2",
        PostalCode = 22222,
        City = "Teststad"
    };
    private readonly Contact testContact1 = new()
    {
        Id = "1",
        FirstName = "Test1", LastName = "Testsson1",
        Email = "test1@domain.com", PhoneNumber = "1111111111",
        StreetAddress = "Test", PostalCode = 11111, City = "Teststad"
    };
    private readonly Contact testContact2 = new()
    {
        Id = "2",
        FirstName = "Test2", LastName = "Testsson2",
        Email = "test2@domain.com", PhoneNumber = "2121212121",
        StreetAddress = "Test", PostalCode = 22222, City = "Teststad"
    };
    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        
        // _fileServiceMock = new Mock<IFileService>();
        // _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns([]);
        // _fileServiceMock.Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>())).Returns(true);
        // _contactService = new ContactService(_fileServiceMock.Object);

        // _fileService2Mock = new Mock<IFileService>();
        // _fileService2Mock.Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>())).Returns(false);
        // _contactService2 = new ContactService(_fileService2Mock.Object);
    }

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
        var contactService = new ContactService(_fileServiceMock.Object);
        
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
        var contactService = new ContactService(_fileServiceMock.Object);
        
        // Act
        var result = contactService.AddContact(_contact1);

        // Assert
        Assert.False(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Never);
    }

    //Fixa
    [Fact]
    public void AddContact_ShouldAddContactDetailsToList()
    {
        
    }

    [Fact]
    public void GetAll_ShouldReturnIEnumerableList()
    {
        // Arrange
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns([]);
        var contactService = new ContactService(_fileServiceMock.Object);

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
        List<Contact> testList = [testContact1, testContact2];
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        var contactService = new ContactService(_fileServiceMock.Object);
        
        // Act
        Contact? result = contactService.GetContactById(testContact2.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        Assert.Equal(result.Id, testContact2.Id);
        Assert.Equal(result.FirstName, testContact2.FirstName);
    }
    
    [Fact]
    public void UpdateContact_ShouldReturnTrue_WhenContactIsUpdatedInList_WithTheNewParameterData()
    {
        // Arrange
        List<Contact> testList = [testContact1];
        var id = testList[0].Id;
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(true);
        var contactService = new ContactService(_fileServiceMock.Object);

        // Act
        bool result = contactService.UpdateContact(id, _updatedContact);

        //Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Once);
    }

    //Fixa
    [Fact]
    public void UpdateContact_ShouldAddNewContactDetailsToTheChosenContactInTheList()
    {
        
    }
    
    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactIsDeletedFromList()
    {
        // Arrange
        List<Contact> testList = [testContact1];
        var id = testList[0].Id;
        
        _fileServiceMock
            .Setup(fs => fs.LoadListFromFile())
            .Returns(testList);
        _fileServiceMock
            .Setup(fs => fs.AddListToFile(testList))
            .Returns(true);
        var contactService = new ContactService(_fileServiceMock.Object);

        // Act
        bool result = contactService.DeleteContact(id);

        //Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(testList), Times.Once);
    }

    // Fixa
    [Fact]
    public void DeleteContact_ShouldDeleteOnlyTheChosenContactFromTheList()
    {
        
    }
}
    
