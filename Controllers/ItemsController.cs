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
        public IEnumerable<ItemDTO> GetItems()
        {
            return _repository.GetItems().Select(item => item.AsDTO());
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDTO();
        }

        // POST /items
        [HttpPost]
        public ActionResult<ItemDTO> CreateItem(CreateItemDTO createItemDTO)
        {
            Item item = new Item
            {
                Id = Guid.NewGuid(),
                Name = createItemDTO.Name,
                Price = createItemDTO.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDTO());
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDTO updateItemDTO)
        {
            var item = _repository.GetItem(id);

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

            _repository.UpdateItem(updated);

            return NoContent();
        }

        // DELETE items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var item = _repository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            _repository.DeleteItem(id);

            return NoContent();
        }
    }
}
