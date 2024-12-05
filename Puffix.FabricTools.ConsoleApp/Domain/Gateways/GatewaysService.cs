using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.Gateways.Models;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.Gateways;

public class GatewaysService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    IGatewaysService
{
    private const string BASE_COMMAND = "gateways";
    private const string BASE_FILE_NAME = "gateways";

    public async Task<IGatewayCommandResult<GatewayList>> List()
    {
        GatewayList gatewayList = await GetElementCollection<GatewayList, Gateway>(BASE_COMMAND);

        string filePath = await SaveContent(BASE_FILE_NAME, gatewayList);

        return IGatewayCommandResult<GatewayList>.CreateNew(filePath, gatewayList.Elements.Count, gatewayList);
    }
}