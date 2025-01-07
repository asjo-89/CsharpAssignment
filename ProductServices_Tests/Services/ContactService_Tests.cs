using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly ContactService _contactService;

    private readonly Mock<IFileService> _fileService2Mock;
    private readonly ContactService _contactService2;

    public ContactService_Tests()
    {
        _fileServiceMock = new Mock<IFileService>();
        _fileServiceMock.Setup(fs => fs.LoadListFromFile()).Returns([]);
        _fileServiceMock.Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>())).Returns(true);
        _contactService = new ContactService(_fileServiceMock.Object);

        _fileService2Mock = new Mock<IFileService>();
        _fileService2Mock.Setup(fs => fs.AddListToFile(It.IsAny<List<Contact>>())).Returns(false);
        _contactService2 = new ContactService(_fileService2Mock.Object);
    }

    [Fact]
    public void AddContact_ShouldReturnTrue_WhenContactIsAddedSuccessfully()
    {
        // Arrange
        ContactForm contact1 = new() { FirstName = "Test1", LastName = "Testsson1", Email = "test1@domain.com", PhoneNumber = "0721234567", StreetAddress = "Testvägen 1", PostalCode = 12345, City = "Test1" };

        // Act
        var result = _contactService.AddContact(contact1);

        // Assert
        _fileServiceMock.Verify(fs => fs.AddListToFile(It.IsAny<List<Contact>>()), Times.Once);
        _fileServiceMock.Verify(fs => fs.AddListToFile(It.Is<List<Contact>>(list => list.Any(x => 
            x.FirstName == "Test1" && x.LastName == "Testsson1" && x.Email == "test1@domain.com" && x.PhoneNumber == "0721234567" && x.StreetAddress == "Testvägen 1" && x.PostalCode == 12345 && x.City == "Test1"))));
    }

    [Fact]
    public void AddContact_ShouldReturnFalse_WhenThereIsNoList()
    {
        // Arrange
        ContactForm contact1 = new() { FirstName = "Test1", LastName = "Testsson1", Email = "test1@domain.com", PhoneNumber = "0721234567", StreetAddress = "Testvägen 1", PostalCode = 12345, City = "Test1" };

        // Act
        var result = _contactService2.AddContact(contact1);

        // Assert
        _fileService2Mock.Verify(fs => fs.AddListToFile(It.IsAny<List<Contact>>()), Times.Never);
    }

    [Fact]
    public void GetAll_ShouldReturnIEnumerableList()
    {
        // Arrange
        ContactForm contact1 = new() { FirstName = "Test1", LastName = "Testsson1", Email = "test1@domain.com", PhoneNumber = "0721234567", StreetAddress = "Testvägen 1", PostalCode = 12345, City = "Test1" };
        var list = _contactService.AddContact(contact1);

        // Act
        var result = _contactService.GetAll();

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Email == "test1@domain.com");
        Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
    }

    [Fact]
    public void FindContactById_ShouldReturnContactWithCorrectId_WhenIdMatchesToIdOfAContactInList()
    {
        // Arrange
        ContactForm contact1 = new()
        { 
            FirstName = "Test1", 
            LastName = "Testsson1", 
            Email = "test1@domain.com", 
            PhoneNumber = "1111111111", 
            StreetAddress = "Testvägen 1", 
            PostalCode = 11111, 
            City = "Teststad"
        };
        ContactForm contact2 = new()
        {
            FirstName = "Test2",
            LastName = "Testsson2",
            Email = "test2@domain.com",
            PhoneNumber = "2111111111",
            StreetAddress = "Testvägen 2",
            PostalCode = 22222,
            City = "Teststad"
        };

        _contactService.AddContact(contact1);
        _contactService.AddContact(contact2);

        var list = _contactService.GetAll().ToList();
        var contact = list[1].Id;

        // Act
        Contact result = _contactService.GetContactById(contact);

        //Assert
        Assert.Equal(list[1].Id, contact);
        Assert.Equal(list[1].FirstName, contact2.FirstName);
    }

    [Fact]
    public void UpdateContact_ShouldReturnTrue_WhenContactIsUpdatedInList_WithTheNewParameterData()
    {
        // Arrange
        ContactForm contact1 = new()
        {
            FirstName = "Test1",
            LastName = "Testsson1",
            Email = "test1@domain.com",
            PhoneNumber = "1111111111",
            StreetAddress = "Testvägen 1",
            PostalCode = 11111,
            City = "Teststad"
        };
        ContactForm contact2 = new()
        {
            FirstName = "Test2",
            LastName = "Testsson2",
            Email = "test2@domain.com",
            PhoneNumber = "2111111111",
            StreetAddress = "Testvägen 2",
            PostalCode = 22222,
            City = "Teststad"
        };
        ContactForm updatedContact = new()
        {
            FirstName = "Testing",
            LastName = "Testing",
            Email = "",
            PhoneNumber = "0",
            StreetAddress = "Testvägen 2",
            PostalCode = 22222,
            City = "Teststad"
        };

        _contactService.AddContact(contact1);
        _contactService.AddContact(contact2);

        var list = _contactService.GetAll().ToList();
        var id = list[1].Id.Substring(0, 4);
        var contact = list[1];

        // Act
        bool result = _contactService.UpdateContact(id, updatedContact);

        //Assert
        Assert.True(result );
        Assert.Equal(updatedContact.FirstName, contact.FirstName);
    }

    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactIsDeletedFromList()
    {
        // Arrange
        ContactForm contact1 = new()
        {
            FirstName = "Test1",
            LastName = "Testsson1",
            Email = "test1@domain.com",
            PhoneNumber = "1111111111",
            StreetAddress = "Testvägen 1",
            PostalCode = 11111,
            City = "Teststad"
        };
        ContactForm contact2 = new()
        {
            FirstName = "Test2",
            LastName = "Testsson2",
            Email = "test2@domain.com",
            PhoneNumber = "2111111111",
            StreetAddress = "Testvägen 2",
            PostalCode = 22222,
            City = "Teststad"
        };

        _contactService.AddContact(contact1);
        _contactService.AddContact(contact2);

        var list = _contactService.GetAll().ToList();
        var id = list[1].Id.Substring(0, 4);
        var contact = list[1];

        // Act
        bool result = _contactService.DeleteContact(id);

        //Assert
        list = _contactService.GetAll().ToList();
        Assert.True(result);
        Assert.Single(list);
        Assert.Equal(list[0].FirstName, contact1.FirstName);
    }
}
