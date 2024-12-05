using System.Text.Json;

namespace Puffix.FabricTools.ConsoleApp.Infra.Files;

public class FileService : IFileService
{
    public bool ExistsFile(string filePath)
    {
        return File.Exists(filePath);
    }

    public bool ExistsDirectory(string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    public string BuildUniqueFileName(string fileName)
    {
        string baseFileName = Path.GetFileNameWithoutExtension(fileName);
        string fileExtension = Path.GetExtension(fileName);

        fileExtension = fileExtension.Trim('.');

        const string DATA_FORMAT = "yyyyMMdd-HHmmss";
        string guid = Guid.NewGuid().ToString("N");

        return $"{baseFileName}-{DateTime.UtcNow.ToString(DATA_FORMAT)}-{guid}.{fileExtension}";
    }

    public string BuildFilePathAndEnsureDirectoryExists(string directoryPath, string fileName)
    {
        directoryPath = !Path.IsPathFullyQualified(directoryPath) ?
                Path.GetFullPath(directoryPath) :
                directoryPath;

        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        return BuildFilePath(directoryPath, fileName);
    }

    public string BuildFilePath(string directoryPath, string fileName)
    {
        directoryPath = !Path.IsPathFullyQualified(directoryPath) ?
                        Path.GetFullPath(directoryPath) :
                        directoryPath;

        return Path.Combine(directoryPath, fileName);
    }

    public async Task<string> LoadContent(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }

    public async Task SaveContent(string filePath, string fileContent)
    {
        await File.WriteAllTextAsync(filePath, fileContent);
    }

    public async Task SaveJsonContent<ObjectT>(string filePath, ObjectT fileContent, bool indent)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = indent
        };

        string serializeContent = JsonSerializer.Serialize(fileContent, options);

        await SaveContent(filePath, serializeContent);
    }
}
