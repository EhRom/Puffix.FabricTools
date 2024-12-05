using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.RestApi;

public class BaseRestFabricService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService)
{
    protected readonly IMsApiRestToken token = token;
    protected readonly IMsApiRestHttpRepository httpRepository = httpRepository;
    protected readonly IFileService fileService = fileService;

    private readonly Lazy<string> baseUrlLazy = new(() =>
    {
        return configuration["fabricApiBaseUrl"]!;
    });

    private readonly Lazy<string> outputDirectoryLazy = new(() =>
    {
        return configuration[nameof(outputDirectory)]!;
    });

    private readonly Lazy<bool> indentJsonLazy = new(() =>
    {
        return configuration.GetValue<bool>(nameof(indentJson));
    });

    protected string baseUrl => baseUrlLazy.Value;

    private string outputDirectory => outputDirectoryLazy.Value;

    private bool indentJson => indentJsonLazy.Value;

    protected async Task<ResultT> GetElement<ResultT>(string command)
        where ResultT : class, new()
    {
        IMsApiRestHttpQueryInformation queryInformation = httpRepository.BuildAuthenticatedQuery(token, HttpMethod.Get, baseUrl, command, string.Empty, string.Empty);

        return await httpRepository.HttpJsonAsync<ResultT>(queryInformation);
    }

    protected async Task<CollectionResultT> GetElementCollection<CollectionResultT, ResultT>(string command)
        where CollectionResultT : BaseFabricList<ResultT>
        where ResultT : class, new()
    {

        return await GetElementCollection<CollectionResultT, ResultT>(command, string.Empty);
    }

    private async Task<CollectionResultT> GetElementCollection<CollectionResultT, ResultT>(string command, string continuationToken)
        where CollectionResultT : BaseFabricList<ResultT>
        where ResultT : class, new()
    {
        string queryParameters = string.IsNullOrEmpty(continuationToken) ? string.Empty : $"continuationToken={continuationToken}";

        IMsApiRestHttpQueryInformation queryInformation = httpRepository.BuildAuthenticatedQuery(token, HttpMethod.Get, baseUrl, command, queryParameters, string.Empty);

        CollectionResultT result = await httpRepository.HttpJsonAsync<CollectionResultT>(queryInformation);

        if (!string.IsNullOrEmpty(result.ContinutationToken))
        {
            CollectionResultT nextResult = await GetElementCollection<CollectionResultT, ResultT>(command, result.ContinutationToken);

            result.Elements = [.. nextResult.Elements];
            result.ContinutationUri = string.Empty;
            result.ContinutationToken = string.Empty;
        }

        return result;
    }

    protected async Task<string> SaveContent<ObjectT>(string baseFileName, ObjectT content)
        where ObjectT : class
    {
        string fileName = fileService.BuildUniqueFileName($"{baseFileName}.{IFileService.JSON_EXTENSION}");
        string filePath = fileService.BuildFilePathAndEnsureDirectoryExists(outputDirectory, fileName);

        await fileService.SaveJsonContent(filePath, content, indentJson);

        return filePath;
    }
}