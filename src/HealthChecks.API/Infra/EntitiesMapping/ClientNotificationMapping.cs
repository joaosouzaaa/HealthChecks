using HealthChecks.API.Entities;
using MongoDB.Bson.Serialization;

namespace HealthChecks.API.Infra.EntitiesMapping;

internal static class ClientNotificationMapping
{
    internal static void Map()
    {
        BsonClassMap.RegisterClassMap<ClientNotification>(options =>
        {
            options.AutoMap();
            options.MapIdField(c => c.Id);
        });
    }
}
