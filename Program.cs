using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CampusPlacement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<CosmosClient>(InitializeCosmosClientInstanceAsync(builder.Configuration).GetAwaiter().GetResult());
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<ICandidateApplicationService, CandidateApplicationService>();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.Run();

static async Task<CosmosClient> InitializeCosmosClientInstanceAsync(IConfiguration configuration)
{
    var account = configuration["CosmosDb:Account"];
    var key = configuration["CosmosDb:Key"];
    var client = new CosmosClient(account, key);
    var databaseName = configuration["CosmosDb:DatabaseName"];
    var containerName = configuration["CosmosDb:ContainerName"];
    var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
    return client;
}
