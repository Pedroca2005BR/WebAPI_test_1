using Microsoft.AspNetCore.Mvc;
using WebAPI_test_1.DTOs;
using WebAPI_test_1.Models;
using WebAPI_test_1.Repositories;

namespace WebAPI_test_1.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
        {
            _repository = repository;
        }

        // Get /items ativa essa funcao
        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            // We need to wrap this up in parentheses to tell the computer to wait for the Task to be returned to use the Select method
            return (await _repository.GetItemsAsync()).Select(item => item.AsDTO());
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDTO();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO createItemDTO)
        {
            Item item = new Item
            {
                Id = Guid.NewGuid(),
                Name = createItemDTO.Name,
                Price = createItemDTO.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDTO());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO updateItemDTO)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {  
                return NotFound(); 
            }

            // Como funciona "with" : Faz uma cópia de um record, mudando algumas variáveis, especificadas dentro das chaves
            Item updated = item with
            {
                Name = updateItemDTO.Name,
                Price = updateItemDTO.Price,
            };

            await _repository.UpdateItemAsync(updated);

            return NoContent();
        }

        // DELETE items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
