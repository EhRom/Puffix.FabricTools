namespace Puffix.FabricTools.ConsoleApp.Infra.Files;

public interface IFileService
{
    internal const string JSON_EXTENSION = "json";

    bool ExistsFile(string filePath);

    bool ExistsDirectory(string directoryPath);

    string BuildUniqueFileName(string fileName);

    string BuildFilePathAndEnsureDirectoryExists(string directoryPath, string fileName);

    string BuildFilePath(string directoryPath, string fileName);

    Task<string> LoadContent(string filePath);

    Task<ObjectT> LoadJsonContent<ObjectT>(string filePath)
        where ObjectT : class;

    Task SaveContent(string filePath, string fileContent);

    Task SaveJsonContent<ObjectT>(string filePath, ObjectT fileContent, bool indent)
        where ObjectT: class;
}
