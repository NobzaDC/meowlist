namespace MeowCore.Models.ResponseDTOs
{
    public class ListsResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
        public List<TodosResponseDto>? Todos { get; set; }
    }
}