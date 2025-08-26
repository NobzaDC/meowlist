namespace MeowCore.Models.ResponseDTOs
{
    public class TagsResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }
        public int UserId { get; set; }
    }
}