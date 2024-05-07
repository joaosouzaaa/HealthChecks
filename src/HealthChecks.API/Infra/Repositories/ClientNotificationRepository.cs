using HealthChecks.API.Constants;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HealthChecks.API.Infra.Repositories;

public sealed class ClientNotificationRepository : IClientNotificationRepository
{
    private readonly IMongoCollection<ClientNotification> _collection;

    public ClientNotificationRepository(IOptions<MongoDBOptions> mongoDBOptions)
    {
        var client = new MongoClient(mongoDBOptions.Value.ConnectionString);
        var database = client.GetDatabase(mongoDBOptions.Value.DatabaseName);
        _collection = database.GetCollection<ClientNotification>(CollectionsConstants.ClientNotificationCollection);
    }

    public Task AddAsync(ClientNotification clientNotification) =>
        _collection.InsertOneAsync(clientNotification);

    public Task<List<ClientNotification>> GetAllByClientId(long clientId) =>
        _collection.Find(c => c.ClientId == clientId).ToListAsync();
}
