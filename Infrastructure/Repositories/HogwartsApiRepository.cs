using HogwartsAPI.Controllers.House;
using HogwartsAPI.Domain.House;
using HogwartsAPI.Domain.Trait;
using HogwartsAPI.Domain.Wizard;
using HogwartsAPI.Infrastructure.Database;
using HogwartsAPI.SharedKernel;

namespace HogwartsAPI.Infrastructure.Repositories
{

    public class HogwartsApiRepository(ApplicationDbContext dbContext)
    {
        public async Task<Result> AddAll(HouseDto[] houses)
        {
            foreach (HouseDto house in houses)
            {
                var found = FindByName(house.Name);
                if (found != null && found.Id != house.Id) return HouseErrors.NameNotUnique;
                if (found is null)
                {
                    await dbContext.Houses.AddAsync(house.ToDomain());
                    await dbContext.SaveChangesAsync();
                }

                var traitsToAdd = from t in house.Traits
                                  where !dbContext.Traits.Any(lt => lt.Id == t.Id || lt.Name == t.Name)
                                  select new Trait
                                  {
                                      Name = t.Name,
                                      Id = t.Id
                                  };
                if (traitsToAdd.ToArray().Length > 0)
                {
                    await dbContext.Traits.AddRangeAsync(traitsToAdd);
                    await dbContext.SaveChangesAsync();

                }

                var hTraitsToAdd = from t in house.Traits
                                   where !dbContext.HouseTraits.Any(lt => lt.TraitId == t.Id && lt.HouseId == house.Id) && dbContext.Traits.FirstOrDefault(lt => lt.Id == t.Id) != null
                                   select new HouseTrait
                                   {
                                       TraitId = t.Id,
                                       HouseId = house.Id
                                   };

                if (hTraitsToAdd.ToArray().Length > 0)
                {
                    await dbContext.HouseTraits.AddRangeAsync(hTraitsToAdd);
                    await dbContext.SaveChangesAsync();

                }


            }

            return Success.Create(houses);
        }
        public Result AddAll(Wizard[] wizards)
        {
            var wizardsToAdd = from w in wizards
                               where !dbContext.Wizards.Any(lt => lt.Id == w.Id)
                               select w;

            dbContext.Wizards.AddRange(wizardsToAdd);
            dbContext.SaveChanges();
            return Success.Create(wizardsToAdd);
        }

        private House? FindByName(string name) => dbContext.Houses.Where((h) => h.Name == name).FirstOrDefault();
    }
}