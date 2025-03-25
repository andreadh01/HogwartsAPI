namespace HogwartsAPI.Controllers.House
{
    using HogwartsAPI.Controllers.Trait;
    using HogwartsAPI.Controllers.Wizard;
    using HogwartsAPI.Domain.House;

    public record HouseDto(
            Guid Id,
            string Name,
            string HouseColours,
            string Founder,
            string Animal,
            string Element,
            string Ghost,
            string CommonRoom,
            List<WizardDto>? Students = null,
            List<TraitDto>? Traits = null)
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
    }
    public record CreateHouseDto(
            string Name,
            string HouseColours,
            string Founder,
            string Animal,
            string Element,
            string Ghost,
            string CommonRoom)
    {
        public House ToDomain()
        {
            return new House
            {
                Name = Name,
                HouseColours = HouseColours,
                Founder = Founder,
                Animal = Animal,
                Element = Element,
                Ghost = Ghost,
                CommonRoom = CommonRoom
            };
        }
    }

    public record UpdateHouseDto(
        string? Name,
        string? HouseColours,
        string? Founder,
        string? Animal,
        string? Element,
        string? Ghost,
        string? CommonRoom)
    {
        public House ToDomain(House house)
        {
            house.Name = Name ?? house.Name;
            house.HouseColours = HouseColours ?? house.HouseColours;
            house.Founder = Founder ?? house.Founder;
            house.Animal = Animal ?? house.Animal;
            house.Element = Element ?? house.Element;
            house.Ghost = Ghost ?? house.Ghost;
            house.CommonRoom = CommonRoom ?? house.CommonRoom;
            return house;
        }
    }
}