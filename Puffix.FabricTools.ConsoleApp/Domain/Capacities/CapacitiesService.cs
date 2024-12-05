using Microsoft.Extensions.Configuration;
using Puffix.FabricTools.ConsoleApp.Domain.Capacities.Models;
using Puffix.FabricTools.ConsoleApp.Domain.RestApi;
using Puffix.FabricTools.ConsoleApp.Infra.Files;
using Puffix.FabricTools.ConsoleApp.Infra.MsApiRest;

namespace Puffix.FabricTools.ConsoleApp.Domain.Capacities;

public class CapacitiesService(IConfiguration configuration, IMsApiRestToken token, IMsApiRestHttpRepository httpRepository, IFileService fileService) :
    BaseRestFabricService(configuration, token, httpRepository, fileService),
    ICapacitiesService
{
    private const string BASE_COMMAND = "capacities";
    private const string BASE_FILE_NAME = "capacities";

    public async Task<ICapacityCommandResult<CapacityList>> List()
    {
        CapacityList capacityList = await GetElement<CapacityList>(BASE_COMMAND);

        string filePath = await SaveContent(BASE_FILE_NAME, capacityList);

        return ICapacityCommandResult<CapacityList>.CreateNew(filePath, capacityList.Elements.Count, capacityList);
    }
}
