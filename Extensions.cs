using WebAPI_test_1.DTOs;
using WebAPI_test_1.Models;

namespace WebAPI_test_1
{
    // A classe Extensions permite implementar metodos em outras classes. Muito util para conversões
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item item)
        {
            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}
