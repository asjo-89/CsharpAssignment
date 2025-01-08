using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnNewContactForm()
    {
        // Arrange
        // Act
        ContactForm result = ContactFactory.Create();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContactForm>(result);
    }


    [Fact]
    public void Create_ShouldReturnNewContact()
    {
        // Arrange
        ContactForm contact = new()
        {
            FirstName = "Alice",
            LastName = "Babs",
            Email = "alice@domain.com",
            PhoneNumber = "0712345678",
            StreetAddress = "Gata 1",
            PostalCode = 12345,
            City = "Stockholm"
        };

        // Act
        var result = ContactFactory.Create(contact);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contact.FirstName, result.FirstName);
        Assert.Equal(contact.LastName, result.LastName);
        Assert.Equal(contact.Email, result.Email);
        Assert.Equal(contact.PhoneNumber, result.PhoneNumber);
        Assert.Equal(contact.StreetAddress, result.StreetAddress);
        Assert.Equal(contact.PostalCode, result.PostalCode);
        Assert.Equal(contact.City, result.City);
    }
    
    //Test för att få Exception om Contact Create ej returnerar contact.
}
