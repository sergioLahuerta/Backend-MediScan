namespace MediScan.Core.Interfaces.Services;

public interface IChatService
{
    Task<string> ProcessChatAsync(string sessionId, string userMessage, string? base64Image);
}
