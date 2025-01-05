using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace AndroidMobileFirst.Services;

public class OllamaService
{
    private readonly HttpClient _httpClient;

    public OllamaService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:5000/api/"); // Replace with your API base address
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetResponseAsync(string input)
    {
        var requestContent = new StringContent(JsonConvert.SerializeObject(new { query = input }), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("ollama", requestContent); // Replace "ollama" with your API endpoint

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
            return result.Response;
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
        }
    }

    private class ApiResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}