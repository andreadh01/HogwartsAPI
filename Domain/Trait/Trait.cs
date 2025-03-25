namespace HogwartsAPI.Domain.Trait
{

    public class Trait
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; init; }
    }
}