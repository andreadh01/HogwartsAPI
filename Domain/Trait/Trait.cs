using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Domain.Trait
{
    [Index(nameof(Name), IsUnique = true)]
    public class Trait
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; set; }
    }
}