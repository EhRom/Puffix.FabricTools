namespace Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

public class BaseCommandResult<ResultT>(string resultFilePath, long resultCount, ResultT result) : ICommandResult<ResultT>
    where ResultT : class
{
    public string ResultFilePath => resultFilePath;

    public long ResultCount => resultCount;

    public ResultT Result => result;
}
