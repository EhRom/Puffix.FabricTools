using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.Domains.Models;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.Domains;

public class DomainsService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    IDomainsService
{
    private const string BASE_COMMAND = "admin/domains";
    private const string BASE_FILE_NAME = "domains";

    public async Task<IDomainCommandResult<FabricDomainList>> List()
    {
        FabricDomainList workspaceList = await GetElementCollection<FabricDomainList, FabricDomain>(BASE_COMMAND);

        string filePath = await SaveContent(BASE_FILE_NAME, workspaceList);

        return IDomainCommandResult<FabricDomainList>.CreateNew(filePath, workspaceList.Elements.Count, workspaceList);
    }
}