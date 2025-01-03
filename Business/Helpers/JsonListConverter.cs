using Business.Interfaces;
using Business.Models;
using System.Text.Json;

namespace Business.Helpers;

public static class JsonListConverter
{
    public static string ConvertToJson(List<Contact> list)
    {
        string json = JsonSerializer.Serialize(list);
        return json;
    }

    public static List<Contact> ConvertToList(string json)
    {
        var list = JsonSerializer.Deserialize<List<Contact>>(json);
        return list ?? [];
    }
}
