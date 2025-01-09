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
        // Act
        var result = _contactService.AddContact(_contact1);

        // Assert
        Assert.True(result);
        _fileServiceMock.Verify(fs => fs.AddListToFile(It.IsAny<List<Contact>>()), Times.Once);
        _fileServiceMock.Verify(fs => fs.AddListToFile(It.Is<List<Contact>>(list => list.Any(x => x.FirstName == _contact1.FirstName))));
    }

    [Fact]
    public void AddContact_ShouldReturnFalse_WhenThereIsNoList()
    {
        // Arrange
        // Act
        var result = _contactService2.AddContact(_contact1);

        // Assert
        Assert.False(result);
        _fileService2Mock.Verify(fs => fs.AddListToFile(It.IsAny<List<Contact>>()), Times.Never);
    }

    [Fact]
    public void GetAll_ShouldReturnIEnumerableList()
    {
        // Arrange
        _contactService.AddContact(_contact1);

        // Act
        var result = _contactService.GetAll().ToList();

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Email == _contact1.Email);
        Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
    }

    [Fact]
    public void GetContactById_ShouldReturnContactWithCorrectData_WhenIdMatchesToIdOfAContactInList()
    {
        // Arrange
        _contactService.AddContact(_contact1);
        _contactService.AddContact(_contact2);

        List<Contact> list = _contactService.GetAll().ToList();
        string contactId = list[1].Id;

        // Act
        Contact? result = _contactService.GetContactById(contactId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(list[1].Id, result.Id);
        Assert.Equal(list[1].FirstName, result.FirstName);
    }

    //Separat test för att se om uppdaterad data stämmer?
    [Fact]
    public void UpdateContact_ShouldReturnTrue_WhenContactIsUpdatedInList_WithTheNewParameterData()
    {
        // Arrange
        _contactService.AddContact(_contact1);
        _contactService.AddContact(_contact2);

        var list = _contactService.GetAll().ToList();
        var id = list[1].Id;
        var contact = list[1];

        // Act
        bool result = _contactService.UpdateContact(id, _updatedContact);

        //Assert
        Assert.True(result);
        Assert.Equal(_updatedContact.FirstName, contact.FirstName);
    }

    //Gör separat test för att rätt kontakt tas bort ur listan.
    [Fact]
    public void DeleteContact_ShouldReturnTrue_WhenContactIsDeletedFromList()
    {
        // Arrange
        _contactService.AddContact(_contact1);
        _contactService.AddContact(_contact2);

        List<Contact> list = _contactService.GetAll().ToList();
        string id = list[1].Id;

        // Act
        bool result = _contactService.DeleteContact(id);

        //Assert
        Assert.True(result);
        list = _contactService.GetAll().ToList();
        Assert.Single(list);
        Assert.Equal(list[0].FirstName, _contact1.FirstName);
    }
}
