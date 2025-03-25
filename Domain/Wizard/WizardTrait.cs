namespace HogwartsAPI.Domain.Wizard
{
    using HogwartsAPI.Domain.Trait;
    public class WizardTrait
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required Guid WizardId { get; init; }
        public Wizard? Wizard { get; }

        public required Guid TraitId { get; init; }
        public Trait? Trait { get; }
    }

}
