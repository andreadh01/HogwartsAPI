using HogwartsAPI.Controllers.House;
using HogwartsAPI.Controllers.Trait;
using HogwartsAPI.Controllers.Wizard;
using HogwartsAPI.Domain.House;
using HogwartsAPI.Domain.Trait;
using HogwartsAPI.Domain.Wizard;
using HogwartsAPI.Infrastructure.Database;
using HogwartsAPI.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace HogwartsAPI.Infrastructure.Repositories
{

    public class WizardsRepository(ApplicationDbContext dbContext) : IRepository<Wizard>
    {
        public Result Create(Wizard wizard)
        {
            if (wizard.HouseId is Guid houseId)
            {
                var found = HouseExists(houseId);
                if (found != null) return HouseErrors.NotFound(houseId);
            }

            dbContext.Wizards.Add(wizard);
            dbContext.SaveChanges();
            return Success.Create(wizard);
        }

        public Result Get(Guid wizardId)
        {
            var wizard = dbContext.Wizards.Include(wizard => wizard.Traits)
                .ThenInclude(t => t.Trait)
                .FirstOrDefault(h => h.Id == wizardId);
            if (wizard is null) return WizardErrors.NotFound(wizardId);
            var result = new WizardDto(
                wizard.Id,
                wizard.HouseId,
                wizard.FirstName,
                wizard.LastName,
                wizard.Traits?.Select(t => new TraitDto(t.Id, t.Trait != null ? t.Trait.Name : "")).ToList(),
                GetHouse(wizard.HouseId)
            );
            return Success.Query(result);
        }

        public Result SortingHat(Guid wizardId)
        {
            var wizard = GetByIdWithTraits(wizardId);
            if (wizard is null) return WizardErrors.NotFound(wizardId);
            if (wizard.HouseId is not null) return WizardErrors.WizardHasHouse;

            var houseMatches = dbContext.Houses
                .Include(h => h.Traits)
                .ToList()
                .Select(h => new
                {
                    House = h,
                    MatchCount = h.Traits.Count(t => wizard.Traits != null && wizard.Traits.Any(wt => wt.Id == t.Id))
                })
                .OrderByDescending(hm => hm.MatchCount)
                .ToArray();

            var matchingHouse = houseMatches.FirstOrDefault();

            if (matchingHouse == null || matchingHouse.MatchCount == 0)
            {
                var random = new Random();
                matchingHouse = houseMatches[random.Next(houseMatches.Length)];
            }


            wizard.HouseId = matchingHouse.House.Id;

            dbContext.SaveChanges();
            return Get(wizardId);
        }

        public Result GetAll()
        {
            var wizards = dbContext.Wizards
            .Include(wizard => wizard.Traits)
                .ThenInclude(t => t.Trait)
            .Include(wizard => wizard.House)
            .Select(s => new WizardDto(
                s.Id, 
                s.HouseId, 
                s.FirstName, 
                s.LastName, 
                s.Traits.Select(t => new TraitDto(t.TraitId, t.Trait != null ? t.Trait.Name : "")).ToList(), 
                s.House == null ? 
                null : 
                new HouseDto(
                    s.House.Id,
                    s.House.Name,
                    s.House.HouseColours,
                    s.House.Founder,
                    s.House.Animal,
                    s.House.Element,
                    s.House.Ghost,
                    s.House.CommonRoom,
                    null,
                    null
                )))
            .ToArray();
            return Success.Query(wizards);
        }

        public Result Delete(Guid wizardId)
        {
            var wizard = GetById(wizardId);
            if (wizard is null) return WizardErrors.NotFound(wizardId);
            dbContext.Wizards.Remove(wizard);
            dbContext.SaveChanges();
            return Success.Delete;
        }

        public Result Update(Wizard wizard)
        {
            dbContext.Wizards.Update(wizard);
            dbContext.SaveChanges();
            return Success.Update(wizard);
        }

        public Result InsertTrait(WizardTrait wizardTrait)
        {
            var wizard = Get(wizardTrait.WizardId);
            if (wizard.IsFailure) return wizard;
            var foundTrait = dbContext.Traits.Find(wizardTrait.TraitId);
            if (foundTrait is null) return TraitErrors.NotFound(wizardTrait.TraitId);
            var found = dbContext.WizardTraits.FirstOrDefault(w => w.TraitId == wizardTrait.TraitId && w.WizardId == wizardTrait.WizardId);
            if (found is not null) return WizardErrors.WizardHasTrait;


            dbContext.WizardTraits.Add(wizardTrait);
            dbContext.SaveChanges();

            return Success.Update(new { wizardTrait.WizardId, wizardTrait.TraitId, wizardTrait.Id });
        }

        private House? HouseExists(Guid houseId) => dbContext.Houses.Find(houseId);
        private HouseDto? GetHouse(Guid? houseId)
        {
            if (houseId is not Guid id) return null;
            var h = HouseExists(id);
            return h is null ? null : new HouseDto(
                h.Id,
                h.Name,
                h.HouseColours,
                h.Founder,
                h.Animal,
                h.Element,
                h.Ghost,
                h.CommonRoom);
        }
        public Wizard? GetById(Guid wizardId) => dbContext.Wizards.Find(wizardId);
        public Wizard? GetByIdWithTraits(Guid wizardId) => dbContext.Wizards.Include(w => w.Traits).ThenInclude(t => t.Trait).FirstOrDefault(w => w.Id == wizardId);

    }
}