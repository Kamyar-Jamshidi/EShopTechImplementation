using EShopTI.Product.Command.Core;
using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Infrastructure.Config;
using EShopTI.Product.Common;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace EShopTI.Product.Command.Infrastructure.Data.Repositories;

public class MongoDBEventSourcingCommandRepository<T> : IEventSourcingCommandRepository<EventModel<T>> where T : BaseEventSourcingAggregateRoot
{
    private readonly IMongoCollection<EventModel<T>> _collection;

    public MongoDBEventSourcingCommandRepository(IOptions<MongoDbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

        BsonClassMap.TryRegisterClassMap<EventModel<T>>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapIdProperty(x => x.Id);
        });

        _collection = mongoDatabase.GetCollection<EventModel<T>>(typeof(T).Name);
    }

    public async Task<IEnumerable<EventModel<T>>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventModel<T>>> GetByAggregateIdAsync(string aggregateId, CancellationToken cancellationToken)
    {
        return await _collection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task Insert(EventModel<T> entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }
}
