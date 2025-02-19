namespace WebAPI_test_1.DTOs
{
    // DTOs são usados para que o objeto original não seja mandado para o front-end, mas sim uma cópia.
    // Fazemos isso para manter segurança.
    public record ItemDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
