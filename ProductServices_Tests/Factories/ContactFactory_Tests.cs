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
        ContactForm contactForm = new()
        {
            FirstName = "Alice", LastName = "Babs",
            Email = "alice@domain.com",PhoneNumber = "0712345678",
            StreetAddress = "Gata 1",PostalCode = 12345,
            City = "Stockholm"
        };

        // Act
        var result = ContactFactory.Create(contactForm);

        // Assert
        
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
        
        bool isValidGuid = Guid.TryParse(result.Id, out Guid contactGuid);  
        Assert.True(isValidGuid);
        Assert.NotEqual(Guid.Empty, contactGuid);
        Assert.Equal(contactForm.FirstName, result.FirstName);
        Assert.Equal(contactForm.LastName, result.LastName);
        Assert.Equal(contactForm.Email, result.Email);
        Assert.Equal(contactForm.PhoneNumber, result.PhoneNumber);
        Assert.Equal(contactForm.StreetAddress, result.StreetAddress);
        Assert.Equal(contactForm.PostalCode, result.PostalCode);
        Assert.Equal(contactForm.City, result.City);
    }
}
