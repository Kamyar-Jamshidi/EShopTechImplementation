using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Infrastructure.Config;
using EShopTI.Product.Common;
using EShopTI.Product.Common.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EShopTI.Product.Command.Infrastructure.Data.Repositories;

public class MongoDBCommandRepository<T> : ICommandRepository<T> where T : BaseAggregateRoot
{
    private readonly IMongoCollection<T> _collection;

    public MongoDBCommandRepository(IOptions<MongoDbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

        BsonClassMap.TryRegisterClassMap<BaseEntity>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.MapIdProperty(x => x.Id);
        });

        _collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _collection.AsQueryable().ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task Insert(T entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task Update(T model, CancellationToken cancellationToken)
    {
        await _collection.ReplaceOneAsync(x => x.Id == model.Id, model, cancellationToken: cancellationToken);
    }
}
