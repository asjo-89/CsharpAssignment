using Business.Models;

namespace Business.Interfaces;

public interface IJsonConverter
{
    string ConvertToJson(List<Contact> list);
    List<Contact> ConvertToList(string json);
}
