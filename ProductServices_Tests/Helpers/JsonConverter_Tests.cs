using Business.Converters;
using Business.Models;

namespace Business.Tests.Helpers;

public class JsonConverter_Tests
{
    private readonly DefaultJsonConverter _jsonConverter = new();
    private readonly Contact _contact = new()
    {
        Id = "1", 
        FirstName = "Test", 
        LastName = "Testsson", 
        Email = "test@domain.com", 
        PhoneNumber = "0731234567", 
        StreetAddress = "Testvägen 14", 
        PostalCode = 12345, 
        City = "Teststad"
    };
    
    [Fact]
    public void ConvertToJson_ShouldConvertListToJson()
    {
        // Arrange
        List<Contact> list = [ _contact ];

        // Act
        string result = _jsonConverter.ConvertToJson(list);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.StartsWith("[", result);
        Assert.Contains("Test", result);
    }

    [Fact]
    public void ConvertToList_ShouldConvertJsonToList()
    {
        // Arrange
        List<Contact> list = [ _contact ];
        string json = _jsonConverter.ConvertToJson(list);

        // Act
        List<Contact> result = _jsonConverter.ConvertToList(json);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<Contact>>(result);
        Assert.Equal("1", result[0].Id);
    }
}
