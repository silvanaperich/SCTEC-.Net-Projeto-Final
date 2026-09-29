namespace DeskFlow.Api.Models.DTOs.Erros
{
    public class ErrorResponseDTO(string message)
    {
        public string Message { get; set; } = message;
    }
}