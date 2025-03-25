using HogwartsAPI.Domain.Trait;
using HogwartsAPI.Infrastructure.Database;
using HogwartsAPI.SharedKernel;

namespace HogwartsAPI.Infrastructure.Repositories
{

    public class TraitsRepository(ApplicationDbContext dbContext) : IRepository<Trait>
    {
        public Result Create(Trait trait)
        {
            var found = FindByName(trait.Name);
            if (found != null) return TraitErrors.NameNotUnique;

            dbContext.Traits.Add(trait);
            dbContext.SaveChanges();
            return Success.Create(trait);
        }

        public Result Get(Guid traitId)
        {
            var trait = dbContext.Traits.FirstOrDefault(h => h.Id == traitId);
            if (trait is null) return TraitErrors.NotFound(traitId);
            return Success.Query(trait);
        }

        public Result GetAll()
        {
            var traits = dbContext.Traits
            .ToArray();
            return Success.Query(traits);
        }

        public Result Delete(Guid traitId)
        {
            var response = Get(traitId);
            if (response.IsFailure || response.Success.Data is not Trait trait) return response;
            dbContext.Traits.Remove(trait);
            dbContext.SaveChanges();
            return Success.Delete;
        }

        public Result Update(Trait trait)
        {
            var found = FindByName(trait.Name);
            if (found != null && found.Id != trait.Id) return TraitErrors.NameNotUnique;
            dbContext.Traits.Update(trait);
            dbContext.SaveChanges();
            return Success.Update(trait);
        }

        private Trait? FindByName(string name) => dbContext.Traits.Where((h) => h.Name == name).FirstOrDefault();
        public Trait? GetById(Guid id) => dbContext.Traits.Where((h) => h.Id == id).FirstOrDefault();

    }
}