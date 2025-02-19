using System.ComponentModel.DataAnnotations;

namespace WebAPI_test_1.DTOs
{
    public record CreateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; init; }
    }
}
