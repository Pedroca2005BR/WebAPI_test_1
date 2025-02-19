using WebAPI_test_1.Models;

namespace WebAPI_test_1.Repositories
{
    public interface IItemsRepository
    {
        public IEnumerable<Item> GetItems();
        public Item GetItem(Guid id);
        public void CreateItem(Item item);
        public void UpdateItem(Item item);
        public void DeleteItem(Guid id);
    }
}
