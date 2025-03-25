using HogwartsAPI.Infrastructure.Repositories;
using HogwartsAPI.Infrastructure.Services;
using HogwartsAPI.Web.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsAPI.Controllers.House
{
    using HogwartsAPI.SharedKernel;

    [ApiController]
    [Route("[controller]")]
    public class HousesController(HousesRepository housesRepository, HogwartsApiService hogwartsApiService) : ControllerBase
    {
        private readonly HousesRepository _housesRepository = housesRepository;
        private readonly HogwartsApiService _hogwartsApiService = hogwartsApiService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apiResponse = await _hogwartsApiService.GetAllHousesAsync();
            if (apiResponse.IsFailure) return apiResponse.ToResponse();

            var result = _housesRepository.GetAll();

            return result.ToResponse();
        }

        [HttpGet("{houseId:guid}")]
        public IActionResult Get(Guid houseId)
        {
            var result = _housesRepository.Get(houseId);
            return result.ToResponse();
        }

        [HttpPost]
        public IActionResult Create(CreateHouseDto request)
        {
            var house = request.ToDomain();

            var result = _housesRepository.Create(house);

            return result.ToResponse();
        }

        [HttpPut("{houseId:guid}")]
        public IActionResult Update(Guid houseId, UpdateHouseDto updateHouseDto)
        {
            var house = _housesRepository.GetById(houseId);
            if (house is null) return Result.Failure(Error.NotFound()).ToResponse();
            var updated = updateHouseDto.ToDomain(house);
            var result = _housesRepository.Update(updated);
            return result.ToResponse();
        }

        [HttpDelete]
        public IActionResult Delete(Guid houseId)
        {
            var result = _housesRepository.Delete(houseId);
            return result.ToResponse();
        }
    }
}