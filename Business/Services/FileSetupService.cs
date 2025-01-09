using Business.Interfaces;

namespace Business.Services;

public class FileSetupService : IFileSetupService
{
    public bool DirectoryExists(string directoryPath) => Directory.Exists(directoryPath);

    public void CreateDirectory(string directoryPath) => Directory.CreateDirectory(directoryPath);

    public bool FileExists(string filePath) => File.Exists(filePath);

    public void WriteAllText(string filePath, string content) => File.WriteAllText(filePath, content);

    public string ReadAllText(string filePath) => File.ReadAllText(filePath);
}

