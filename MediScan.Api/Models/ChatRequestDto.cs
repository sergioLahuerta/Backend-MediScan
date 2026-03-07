namespace MediScan.Api.Models
{
    public class ChatRequestDto
    {
        public Guid ChatSessionId { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Base64Image { get; set; }
    }
}