using AndroidMobileFirst.Models;

namespace AndroidMobileFirst.Services
{
    public interface IChatClient
    {
           IAsyncEnumerable<ChatMessage> CompleteStreamingAsync(List<ChatMessage> chatHistory);
    }
}