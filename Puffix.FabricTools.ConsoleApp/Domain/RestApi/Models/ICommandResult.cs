namespace Puffix.FabricTools.ConsoleApp.Domain.RestApi.Models;

public interface ICommandResult<ResultT>
    where ResultT : class
{
    string ResultFilePath { get; }

    long ResultCount { get; }

    ResultT Result { get; }
}
