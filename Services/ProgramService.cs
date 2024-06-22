using CampusPlacement.Models;
using CampusPlacement.Services;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

public class ProgramService : IProgramService
{
    private readonly Container _container;

    public ProgramService(CosmosClient cosmosClient, IConfiguration configuration)
    {
        var databaseName = configuration["CosmosDb:DatabaseName"];
        var containerName = configuration["CosmosDb:ContainerName"];
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task AddProgramAsync(ProgramModel program)
    {
        await _container.CreateItemAsync(program, new PartitionKey(program.Id));
    }

    public async Task UpdateProgramAsync(string id, ProgramModel program)
    {
        await _container.UpsertItemAsync(program, new PartitionKey(id));
    }

    public async Task<ProgramModel> GetProgramByIdAsync(string id)
    {
        try
        {
            var response = await _container.ReadItemAsync<ProgramModel>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
