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

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item newItem)
        {
            var index = items.FindIndex(item => item.Id == newItem.Id);
            items[index] = newItem;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(items => items.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
