namespace HogwartsAPI.Domain.House
{
    using HogwartsAPI.Domain.Trait;
    public class HouseTrait
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required Guid HouseId { get; init; }
        public House? House { get; }

        public required Guid TraitId { get; init; }
        public Trait? Trait { get; }
    }
}