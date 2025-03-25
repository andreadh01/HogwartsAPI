namespace HogwartsAPI.Controllers.Wizard
{
    using HogwartsAPI.Controllers.House;
    using HogwartsAPI.Controllers.Trait;
    using HogwartsAPI.Domain.Wizard;
    public record WizardDto(
            Guid Id,
            Guid? HouseId,
            string? FirstName,
            string LastName,
            List<TraitDto>? Traits = null,
            HouseDto? House = null);
    public record CreateWizardDto(
            string FirstName,
            string LastName)
    {
        public Wizard ToDomain()
        {
            return new Wizard
            {
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }

    public record CreateWizardTraitDto(
        Guid TraitId)
    {
        public WizardTrait ToDomain(Guid wizardId)
        {
            return new WizardTrait
            {
                TraitId = TraitId,
                WizardId = wizardId
            };
        }
    }

    public record UpdateWizardDto(
        string? FirstName,
        string? LastName)
    {
        public Wizard ToDomain(Wizard wizard)
        {
            wizard.FirstName = FirstName ?? wizard.FirstName;
            wizard.LastName = LastName ?? wizard.LastName;
            return wizard;
        }
    }
}