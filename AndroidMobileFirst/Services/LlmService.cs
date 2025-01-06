using System.Text;
using Newtonsoft.Json;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using OllamaSharp.Models.Chat;


namespace AndroidMobileFirst.Services;

public class LlmService
{
    private readonly OllamaChatClient _ollamaChatClient;

    public Uri? ContextUri { get; set; }

    public string? IsSummaryTrue { get; set; }
   
    public LlmService()
    {
        var ollamaUri = new Uri("http://127.0.0.1:11434/api/chat"); // Replace with your actual URI
        var model = "llama3.2:latest"; // Replace with your actual model name
        _ollamaChatClient = new OllamaChatClient(ollamaUri, model);
    }

    public async Task CompletingStreamingAsync(List<Message> chatHistory = null)
    {
    chatHistory = chatHistory.Equals(null) ? new List<Message>() : chatHistory;

    // Load and extract text from the PDF
    string pdfContent = LoadPdfContent(ContextUri.LocalPath);

    // Add the PDF content as context
    chatHistory.Add(new Message(ChatRole.User, pdfContent));

        while (true)
        {
            chatHistory.Add(new Message(ChatRole.User, IsSummaryTrue));

            // Stream the AI response
            Console.WriteLine("AI Response:");
            var response = "";
            await foreach (Message item in _ollamaChatClient.CompleteStreamingAsync(chatHistory))
            {
                Console.Write(item.Content);
                response += item.Content;
            }
            chatHistory.Add(new Message(ChatRole.Assistant, response));
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

    public async Task<string> GetResponseAsync(string input)
        {
            var chatHistory = new List<Message>
            {
                new Message(ChatRole.User, input)
            };

            await foreach (var message in _ollamaChatClient.CompleteStreamingAsync(chatHistory))
            {
                return message.Content;
            }

            return string.Empty;
        }


    private class ApiResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}