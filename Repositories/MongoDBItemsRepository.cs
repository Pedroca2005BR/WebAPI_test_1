using WebAPI_test_1.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WebAPI_test_1.Repositories
{
    public class MongoDBItemsRepository : IItemsRepository
    {
        private readonly IMongoCollection<Item> _itemsCollection;
        private const string databaseName = "Catalog";
        private const string collectionName = "Items";
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDBItemsRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            _itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await _itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(oldItem => oldItem.Id, item.Id);
            await _itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}
