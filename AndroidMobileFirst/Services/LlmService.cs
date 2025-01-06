using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;


namespace AndroidMobileFirst.Services;

public class LlmService
{
    private readonly HttpClient _httpClient;

    public Uri ContextUri { get; set; }

    public async Task CompletingStreamingAsync(List<ChatMessage> chatHistory = null)
    {
    IChatClient chatClient = new OllamaChatClient(_httpClient.BaseAddress!, "phi3:mini");
    chatHistory = chatHistory.Equals(null) ? new List<ChatMessage>() : chatHistory;

    // Load and extract text from the PDF
    string pdfContent = LoadPdfContent(ContextUri.LocalPath);

    // Add the PDF content as context
    chatHistory.Add(new ChatMessage(Enums.ChatRole.User, pdfContent));

        while (true)
        {
            var userPrompt = Console.ReadLine();
            chatHistory.Add(new ChatMessage(Enums.ChatRole.User, userPrompt));

            // Stream the AI response
            Console.WriteLine("AI Response:");
            var response = "";
            await foreach (ChatMessage item in chatClient.CompleteStreamingAsync(chatHistory))
            {
                Console.Write(item.Text);
                response += item.Text;
            }
            chatHistory.Add(new ChatMessage(Enums.ChatRole.Assistant, response));
            Console.WriteLine();
        }
    }

    static string LoadPdfContent(string filePath)
    {
            StringBuilder text = new StringBuilder();

            using (PdfReader pdfReader = new PdfReader(filePath))
            using (PdfDocument pdfDoc = new PdfDocument(pdfReader))
            {
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page)));
                }
            }

            return text.ToString();
        }

    public LlmService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public void SetBaseUrl(string baseUrl)
    {
        _httpClient.BaseAddress = new Uri("http://127.0.0.1:11434"); //new Uri(baseUrl);  should be replaced with the actual URL 
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