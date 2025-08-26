using MeowCore.Helpers;

namespace MeowCore.Models.ResponseDTOs
{
    public class TodosResponseDto
    {
        public TodosResponseDto(Todos notFormattedMode)
        {
            Id = notFormattedMode.Id;
            Title = notFormattedMode.Title;
            Description = notFormattedMode.Description;
            Status = notFormattedMode.Status;
            ListId = notFormattedMode.ListId;
            ListTitle = notFormattedMode.List?.Title;
            Tags = notFormattedMode.TodosTags?.Select(tt => new TodoTagResponseDto
            {
                TagId = tt.Tag.Id,
                TagName = tt.Tag.Name,
                TagColor = tt.Tag.Color
            }).ToList() ?? new List<TodoTagResponseDto>();            
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TodoStatus Status { get; set; } = 0;
        public int ListId { get; set; }
        public string? ListTitle { get; set; }
        public List<TodoTagResponseDto> Tags { get; set; } = new();
    }
}