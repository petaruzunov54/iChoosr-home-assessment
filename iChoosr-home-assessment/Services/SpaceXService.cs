using iChoosr_home_assessment.PayloadModels;
using System.Text.Json;

namespace iChoosr_home_assessment.Services
{
    public class SpaceXService : ISpaceXService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.spacexdata.com/v3/payloads";

        public SpaceXService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Payload>> GetAllPayloadsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var payloads = JsonSerializer.Deserialize<List<Payload>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return payloads ?? new List<Payload>();
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (JsonException ex)
            {
                throw;
            }
        }

        public async Task<Payload?> GetPayloadByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var payload = JsonSerializer.Deserialize<Payload>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return payload;
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            catch (JsonException ex)
            {
                throw;
            }
        }
    }
}
