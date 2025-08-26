namespace MeowCore.Models.ResponseDTOs
{
    public class TodoTagResponseDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string? TagColor { get; set; }
    }
}