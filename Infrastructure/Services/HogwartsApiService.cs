using HogwartsAPI.Controllers.House;
using HogwartsAPI.Domain.Wizard;
using HogwartsAPI.Infrastructure.Repositories;
using HogwartsAPI.SharedKernel;
using Newtonsoft.Json;

namespace HogwartsAPI.Infrastructure.Services
{
    public class HogwartsApiService(HogwartsApiRepository apiRepository, HttpClient httpClient)
    {
        private readonly string apiUrl = "https://wizard-world-api.herokuapp.com/";
        private readonly HogwartsApiRepository _apiRepository = apiRepository;

        private readonly HttpClient _httpClient = httpClient;

        public async Task<Result> GetAllHousesAsync()
        {
            var response = await _httpClient.GetAsync($"{apiUrl}Houses");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Ocurrió un error al intentar consultar la API. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<HouseDto[]>(content);


            if (data is null) return Error.Failure();
            await _apiRepository.AddAll(data);

            return Success.Create(data);
        }
        public async Task<Result> GetAllWizardsAsync()
        {
            var response = await _httpClient.GetAsync($"{apiUrl}Wizards");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Ocurrió un error al intentar consultar la API. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<Wizard[]>(content);

            if (data is null) return Error.Failure();

            _apiRepository.AddAll(data);

            return Success.Create(data);
        }

    }
}
