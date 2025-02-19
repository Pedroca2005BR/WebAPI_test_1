using WebAPI_test_1.Models;

namespace WebAPI_test_1.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new List<Item>()
        {
            new Item {Id = Guid.NewGuid(), Name = "Health Potion", Price = 9.99M, CreatedDate = DateTimeOffset.UtcNow },
            new Item {Id = Guid.NewGuid(), Name = "Mana Potion", Price = 19.99M, CreatedDate = DateTimeOffset.UtcNow },
            new Item {Id = Guid.NewGuid(), Name = "Strength Potion", Price = 49.99M, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return items.Where(item => item.Id == id).SingleOrDefault();
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item newItem)
        {
            var index = items.FindIndex(item => item.Id == newItem.Id);
            items[index] = newItem;
        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(items => items.Id == id);
            items.RemoveAt(index);
        }
    }
}
