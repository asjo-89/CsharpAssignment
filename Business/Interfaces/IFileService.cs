using Business.Models;

namespace Business.Interfaces;

public interface IFileService
{
    bool AddListToFile(List<Contact> list);
    List<Contact> LoadListFromFile();
}
