using Business.Helpers;
using Business.Models;

namespace Business.Tests.Helpers;

public class JsonConverter_Tests
{
    [Fact]
    public void ConvertToJson_ShouldConvertListToJson()
    {
        // Arrange
        var contact = new Contact() { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@domain.com", PhoneNumber = 0731234567, StreetAddress = "Testvägen 14", PostalCode = 12345, City = "TestStad" };
        var list = new List<Contact>();
        list.Add(contact);

        // Act
        var result = JsonListConverter.ConvertToJson(list);

        // Assert
        Assert.NotEmpty(result);
        Assert.StartsWith("[", result);
        Assert.Contains("Test", result);
    }

    [Fact]
    public void ConvertToList_ShouldConvertJsonToList()
    {
        // Arrange
        var contact = new Contact() { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@domain.com", PhoneNumber = 0731234567, StreetAddress = "Testvägen 14", PostalCode = 12345, City = "TestStad" };
        var list = new List<Contact>();
        list.Add(contact);
        var json = JsonListConverter.ConvertToJson(list);

        // Act
        var result = JsonListConverter.ConvertToList(json);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<Contact>>(result);
        Assert.Equal("1", result[0].Id);
    }
}
