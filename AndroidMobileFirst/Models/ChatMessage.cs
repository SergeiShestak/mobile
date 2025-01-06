using AndroidMobileFirst.Enums;

namespace AndroidMobileFirst.Models;

public class ChatMessage
{
    public ChatRole Role { get; set; }
    public string Text { get; set; }

    public ChatMessage(ChatRole role, string text)
    {
        Role = role;
        Text = text;
    }
}