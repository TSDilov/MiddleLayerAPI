using Azure;
using MiddleLayer.Identity.Models;
using MiddleLayer.Infrastructure.Models;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiddleLayer.Tests.Helpers
{
    public class MiddleLayerHttpClient
    {
        public MiddleLayerHttpClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; private set; }

        public async Task<HttpClientResult<RegistrationResponse, string>> Register(RegistrationRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync("account/register", request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<RegistrationResponse>();

                return HttpClientResult<RegistrationResponse, string>.CreateSuccess(responseContent, response.StatusCode);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return HttpClientResult<RegistrationResponse, string>.CreateError(errorContent, response.StatusCode);
        }

        public async Task<HttpClientResult<AuthResponse, string>> Login(AuthRequest request)
        {
            var response = await HttpClient.PostAsJsonAsync("account/login", request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<AuthResponse>();

                return HttpClientResult<AuthResponse, string>.CreateSuccess(responseContent, response.StatusCode);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            return HttpClientResult<AuthResponse, string>.CreateError(errorContent, response.StatusCode);
        }

        public async Task<Character> FetchData()
        {
            var response = await HttpClient.GetAsync("Data");
            var responseContent = await response.Content.ReadAsStringAsync();
            var characterResponse = JsonSerializer.Deserialize<CharacterResponse>(responseContent);

            return characterResponse?.Result;
        }

        public async Task<bool> DeleteData()
        {
            var response = await HttpClient.DeleteAsync("Data/Delete");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to delete data. Status code: {response.StatusCode}. Error message: {errorMessage}");
        }
    }
}
