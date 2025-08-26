using MeowCore.Models;
using MeowCore.Models.RequestDTOs;

namespace MeowCore.Service.Interfaces
{
    public interface ITodosService
    {
        Task<Todos?> GetTodosById(int id);
        Task<List<Todos>?> GetTodosAsync();
        Task<Todos> CreateTodoAsync(TodoRequestDto todo, int user_id);
        Task<Todos?> UpdateTodoAsync(int id, TodoRequestDto todo);
        Task<bool> DeleteTodoAsync(int id);
        Task<Todos?> GetTodoWithTags(int todoId);
        Task AddTagToTodo(int todoId, int tagId);
        Task RemoveTagFromTodo(int todoId, int tagId);
        Task<List<Tags>> GetTagsForTodo(int todoId);
        Task<List<Todos>> GetTodosByUser(int userId);
        Task<List<Todos>> GetTodosByList(int listId);
    }
}