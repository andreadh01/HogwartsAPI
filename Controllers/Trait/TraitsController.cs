using HogwartsAPI.Infrastructure.Repositories;
using HogwartsAPI.Web.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsAPI.Controllers.Trait
{
    using HogwartsAPI.SharedKernel;

    [ApiController]
    [Route("[controller]")]
    public class TraitsController(TraitsRepository traitsRepository) : ControllerBase
    {
        private readonly TraitsRepository _traitsRepository = traitsRepository;

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _traitsRepository.GetAll();

            return result.ToResponse();
        }

        [HttpGet("{traitId:guid}")]
        public IActionResult Get(Guid traitId)
        {
            var result = _traitsRepository.Get(traitId);
            return result.ToResponse();
        }

        [HttpPost]
        public IActionResult Create(CreateTraitDto request)
        {
            var trait = request.ToDomain();

            var result = _traitsRepository.Create(trait);

            return result.ToResponse();
        }

        [HttpPut("{traitId:guid}")]
        public IActionResult Update(Guid traitId, UpdateTraitDto updateTraitDto)
        {
            var trait = _traitsRepository.GetById(traitId);
            if (trait is null) return Result.Failure(Error.NotFound()).ToResponse();
            var updated = updateTraitDto.ToDomain(trait);
            var result = _traitsRepository.Update(updated);
            return result.ToResponse();
        }

        [HttpDelete]
        public IActionResult Delete(Guid traitId)
        {
            var result = _traitsRepository.Delete(traitId);
            return result.ToResponse();
        }
    }
}