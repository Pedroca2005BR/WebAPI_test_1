using WebAPI_test_1.Models;
using MongoDB.Driver;

namespace WebAPI_test_1.Repositories
{
    public class MongoDBItemsRepository : IItemsRepository
    {
        public MongoDBItemsRepository(IMongoClient client)
        {

        }

        public void CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
