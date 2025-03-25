using HogwartsAPI.Domain.House;
using HogwartsAPI.Domain.Trait;

namespace HogwartsAPI.Infrastructure.Services
{
    public record ApiHouseDto(
            Guid Id,
            string Name,
            string HouseColours,
            string Founder,
            string Animal,
            string Element,
            string Ghost,
            string CommonRoom,
            List<Trait> Traits)
    {
        public House ToDomain()
        {
            return new House
            {
                Id = Id,
                Name = Name,
                HouseColours = HouseColours,
                Founder = Founder,
                Animal = Animal,
                Element = Element,
                Ghost = Ghost,
                CommonRoom = CommonRoom
            };
        }

        public HouseTrait CreateHouseTrait(Guid traitId)
        {
            return new HouseTrait
            {
                HouseId = Id,
                TraitId = traitId
            };
        }
    }
}
