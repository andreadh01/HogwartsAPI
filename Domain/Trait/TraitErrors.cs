using HogwartsAPI.SharedKernel;
namespace HogwartsAPI.Domain.Trait
{
    public static class TraitErrors
    {
        public static Error NotFound(Guid houseId) => Error.NotFound($"No se encontró un atributo con el Id: '{houseId}'");
        public static readonly Error NameNotUnique = Error.Conflict("El nombre del atributo ya está registrado");
    }
}