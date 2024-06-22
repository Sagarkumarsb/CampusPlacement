using CampusPlacement.Models;
using CampusPlacement.Services;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

public class CandidateApplicationService : ICandidateApplicationService
{
    private readonly Container _container;

    public CandidateApplicationService(CosmosClient cosmosClient, IConfiguration configuration)
    {
        var databaseName = configuration["CosmosDb:DatabaseName"];
        var containerName = configuration["CosmosDb:ContainerName"];
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task SubmitApplicationAsync(CandidateApplication application)
    {
        await _container.CreateItemAsync(application, new PartitionKey(application.Id));
    }

    public async Task<CandidateApplication> GetApplicationByIdAsync(int id)
    {
        try
        {
            var response = await _container.ReadItemAsync<CandidateApplication>(id.ToString(), new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}
