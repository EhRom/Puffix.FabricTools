using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.Connections.Models;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.Connections;

public class ConnectionsService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    IConnectionsService
{
    private const string BASE_COMMAND = "connections";
    private const string BASE_FILE_NAME = "connections";

    public async Task<IConnectionCommandResult<ConnectionList>> List()
    {
        ConnectionList connectionList = await GetElementCollection<ConnectionList, Connection>(BASE_COMMAND);

        string filePath = await SaveContent(BASE_FILE_NAME, connectionList);

        return IConnectionCommandResult<ConnectionList>.CreateNew(filePath, connectionList.Elements.Count, connectionList);
    }
}