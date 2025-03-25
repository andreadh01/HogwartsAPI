using HogwartsAPI.SharedKernel;
namespace HogwartsAPI.Domain.Wizard
{
    public static class WizardErrors
    {
        public static Error NotFound(Guid wizardId) => Error.NotFound($"No se encontró un mago con el Id: '{wizardId}'");
        public static Error WizardHasHouse => Error.Conflict("El mago ya tiene una casa asignada, no hay cambios de casa");
        public static Error WizardHasTrait => Error.Conflict("El mago ya tiene ese atributo");

    }
}