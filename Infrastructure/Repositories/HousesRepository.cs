using HogwartsAPI.Controllers.House;
using HogwartsAPI.Controllers.Trait;
using HogwartsAPI.Controllers.Wizard;
using HogwartsAPI.Domain.House;
using HogwartsAPI.Infrastructure.Database;
using HogwartsAPI.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Infrastructure.Repositories
{

    public class HousesRepository(ApplicationDbContext dbContext) : IRepository<House>
    {
        public Result Create(House house)
        {
            var found = FindByName(house.Name);
            if (found != null) return HouseErrors.NameNotUnique;
            dbContext.Houses.Add(house);
            dbContext.SaveChanges();
            return Success.Create(house);
        }

        public Result Get(Guid houseId)
        {
            var house = dbContext.Houses
                .Where(h => h.Id == houseId)
                .Include(h => h.Students)
                .Include(h => h.Traits)
                    .ThenInclude(t => t.Trait)
                .AsEnumerable()
                .Select(h => new HouseDto(
                    h.Id,
                    h.Name,
                    h.HouseColours,
                    h.Founder,
                    h.Animal,
                    h.Element,
                    h.Ghost,
                    h.CommonRoom,
                    h.Students.Select(s => new WizardDto(s.Id, h.Id, s.FirstName, s.LastName)).ToList(),
                    h.Traits.Select(t => new TraitDto(t.TraitId, t.Trait != null ? t.Trait.Name : "")).ToList()
                )).FirstOrDefault();
            if (house is null) return HouseErrors.NotFound(houseId);
            return Success.Query(house);
        }

        public Result GetAll()
        {
            var houses = dbContext.Houses
                .Include(h => h.Students)
                .Include(h => h.Traits)
                    .ThenInclude(t => t.Trait)
                .AsEnumerable()
                .Select(h => new HouseDto(
                    h.Id,
                    h.Name,
                    h.HouseColours,
                    h.Founder,
                    h.Animal,
                    h.Element,
                    h.Ghost,
                    h.CommonRoom,
                    h.Students.Select(s => new WizardDto(s.Id, h.Id, s.FirstName, s.LastName)).ToList(),
                    h.Traits.Select(t => new TraitDto(t.TraitId, t.Trait != null ? t.Trait.Name : "")).ToList()
                ));
            return Success.Query(houses);
        }

        public Result Delete(Guid houseId)
        {
            var house = GetById(houseId);
            if (house is null) return HouseErrors.NotFound(houseId);
            dbContext.Houses.Remove(house);
            dbContext.SaveChanges();
            return Success.Delete;
        }

        public Result Update(House house)
        {
            var found = FindByName(house.Name);
            if (found != null && found.Id != house.Id) return HouseErrors.NameNotUnique;
            dbContext.Houses.Update(house);
            dbContext.SaveChanges();
            return Success.Update(house);
        }

        private House? FindByName(string name) => dbContext.Houses.Where((h) => h.Name == name).FirstOrDefault();
        public House? GetById(Guid id) => dbContext.Houses.Where((h) => h.Id == id).FirstOrDefault();

    }
}