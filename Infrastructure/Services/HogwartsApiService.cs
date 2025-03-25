using HogwartsAPI.Domain.House;
using HogwartsAPI.Domain.Wizard;
using Newtonsoft.Json;

namespace HogwartsAPI.Infrastructure.Services
{
    public class HogwartsApiService(HttpClient httpClient)
    {
        private readonly string apiUrl = "https://wizard-world-api.herokuapp.com/";

        private readonly HttpClient _httpClient = httpClient;

        public async Task<House[]> GetAllHousesAsync()
        {
            var response = await _httpClient.GetAsync($"{apiUrl}Houses");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Ocurrió un error al intentar consultar la API. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            
            var data = JsonConvert.DeserializeObject<House[]>(content);

            return data ?? [];
        }

        public async Task<Wizard[]> GetAllWizardsAsync()
        {
            var response = await _httpClient.GetAsync($"{apiUrl}Wizards");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Ocurrió un error al intentar consultar la API. Status code: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            
            var data = JsonConvert.DeserializeObject<Wizard[]>(content);

            return data ?? [];
        }
    }
}
