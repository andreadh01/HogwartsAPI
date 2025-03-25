namespace HogwartsAPI.Controllers.Trait
{

    using HogwartsAPI.Domain.Trait;

    public record TraitDto(
        Guid Id,
        string Name);
    public record CreateTraitDto(
            string Name)
    {
        public Trait ToDomain()
        {
            return new Trait
            {
                Name = Name
            };
        }
    }

    public record UpdateTraitDto(
        string Name)
    {
        public Trait ToDomain(Trait trait)
        {
            trait.Name = Name;
            return trait;
        }
    }

}