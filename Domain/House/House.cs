using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Domain.House
{
    using HogwartsAPI.Domain.Wizard;

    [Index(nameof(Name), IsUnique = true)]
    public class House
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string HouseColours { get; set; }
        public required string Founder { get; set; }
        public required string Animal { get; set; }
        public required string Element { get; set; }
        public required string Ghost { get; set; }
        public required string CommonRoom { get; set; }
        public List<HouseTrait> Traits { get; } = [];
        public List<Wizard> Students { get; } = [];

    }
}