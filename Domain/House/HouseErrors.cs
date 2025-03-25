using HogwartsAPI.SharedKernel;
namespace HogwartsAPI.Domain.House
{
    public static class HouseErrors
    {
        public static Error NotFound(Guid houseId) => Error.NotFound($"No se encontró una casa con el Id: '{houseId}'");
        public static readonly Error NameNotUnique = Error.Conflict("El nombre de la casa ya está registrado");
    }
}