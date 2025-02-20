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

        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            _itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return _itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(oldItem => oldItem.Id, item.Id);
            _itemsCollection.ReplaceOne(filter, item);
        }
    }
}
