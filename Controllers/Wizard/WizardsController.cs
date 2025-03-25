using HogwartsAPI.Infrastructure.Repositories;
using HogwartsAPI.Infrastructure.Services;
using HogwartsAPI.Web.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsAPI.Controllers.Wizard
{
    using HogwartsAPI.Domain.Wizard;
    using HogwartsAPI.SharedKernel;

    [ApiController]
    [Route("[controller]")]
    public class WizardsController(WizardsRepository wizardsRepository, HogwartsApiService hogwartsApiService) : ControllerBase
    {
        private readonly WizardsRepository _wizardsRepository = wizardsRepository;
        private readonly HogwartsApiService _hogwartsApiService = hogwartsApiService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiResponse = await _hogwartsApiService.GetAllWizardsAsync();
            if (apiResponse.IsFailure) return apiResponse.ToResponse();
            var result = _wizardsRepository.GetAll();

            return result.ToResponse();
        }

        [HttpPut("{wizardId:guid}/SortingHat")]
        public IActionResult SortingHat(Guid wizardId)
        {
            var result = _wizardsRepository.SortingHat(wizardId);
            return result.ToResponse();
        }

        [HttpGet("{wizardId:guid}")]
        public IActionResult Get(Guid wizardId)
        {
            var result = _wizardsRepository.Get(wizardId);
            return result.ToResponse();
        }

        [HttpPost]
        public IActionResult Create(CreateWizardDto request)
        {
            var wizard = request.ToDomain();

            var result = _wizardsRepository.Create(wizard);

            return result.ToResponse();
        }

        [HttpPut("{wizardId:guid}")]
        public IActionResult Update(Guid wizardId, UpdateWizardDto updateWizardDto)
        {
            var wizard = _wizardsRepository.GetById(wizardId);
            if (wizard is null) return Result.Failure(Error.NotFound()).ToResponse();
            var updated = updateWizardDto.ToDomain(wizard);
            var result = _wizardsRepository.Update(updated);
            return result.ToResponse();
        }

        [HttpPut("{wizardId:guid}/trait")]
        public IActionResult InsertTrait(Guid wizardId, CreateWizardTraitDto createWizardTraitDto)
        {
            var updated = createWizardTraitDto.ToDomain(wizardId);
            var result = _wizardsRepository.InsertTrait(updated);
            return result.ToResponse();
        }

        [HttpDelete]
        public IActionResult Delete(Guid wizardId)
        {
            var result = _wizardsRepository.Delete(wizardId);
            return result.ToResponse();
        }
    }
}