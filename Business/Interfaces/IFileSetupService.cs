namespace Business.Interfaces;

public interface IFileSetupService
{
    bool DirectoryExists(string directoryPath);
    void CreateDirectory(string directoryPath);
    bool FileExists(string filePath);
    void WriteAllText(string filePath, string content);
    string ReadAllText(string filePath);
}