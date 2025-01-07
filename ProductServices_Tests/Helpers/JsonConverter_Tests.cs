using Business.Helpers;
using Business.Models;

namespace Business.Tests.Helpers;

public class JsonConverter_Tests
{
    [Fact]
    public void ConvertToJson_ShouldConvertListToJson()
    {
        // Arrange
        Contact contact = new() { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@domain.com", PhoneNumber = "0731234567", StreetAddress = "Testvägen 14", PostalCode = 12345, City = "TestStad" };
        List<Contact> list =
        [
            contact
        ];

        // Act
        string result = JsonListConverter.ConvertToJson(list);

        // Assert
        Assert.NotEmpty(result);
        Assert.StartsWith("[", result);
        Assert.Contains("Test", result);
    }

    [Fact]
    public void ConvertToList_ShouldConvertJsonToList()
    {
        // Arrange
        Contact contact = new() { Id = "1", FirstName = "Test", LastName = "Testsson", Email = "test@domain.com", PhoneNumber = "0731234567", StreetAddress = "Testvägen 14", PostalCode = 12345, City = "TestStad" };
        List<Contact> list =
        [
            contact
        ];
        string json = JsonListConverter.ConvertToJson(list);

        // Act
        List<Contact> result = JsonListConverter.ConvertToList(json);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<Contact>>(result);
        Assert.Equal("1", result[0].Id);
    }
}
