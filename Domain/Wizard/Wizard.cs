namespace HogwartsAPI.Domain.Wizard
{
    using HogwartsAPI.Domain.House;

    public class Wizard
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required Guid HouseId { get; init; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public List<WizardTrait> Traits { get; } = [];
        public House? House { get; }

    }
}