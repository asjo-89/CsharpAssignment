using Business.Models;
using System.Text.Json;
using Business.Interfaces;

namespace Business.Helpers;

// public abstract class JsonListConverter : IJsonConverter
// {
//     public string ConvertToJson(List<Contact> list)
//     {
//         string json = JsonSerializer.Serialize(list);
//         return json;
//     }
//
//     public List<Contact> ConvertToList(string json)
//     {
//         List<Contact>? list = JsonSerializer.Deserialize<List<Contact>>(json);
//         return list ?? [];
//     }
// }

public class JsonListConverter : IJsonConverter
{
    public string ConvertToJson(List<Contact> list)
    {
        string json = JsonSerializer.Serialize(list);
        return json;
    }

    public List<Contact> ConvertToList(string json)
    {
        List<Contact>? list = JsonSerializer.Deserialize<List<Contact>>(json);
        return list ?? [];
    }
}
