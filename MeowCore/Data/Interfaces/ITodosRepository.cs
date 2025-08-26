using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Data.Interfaces
{
    public interface ITodosRepository
    {
        Task<Todos?> GetTodosById(int id);
        Task<List<Todos>?> GetTodosAsync();
        Task<Todos> CreateTodoAsync(Todos todo, int user_id);
        Task<Todos?> UpdateTodoAsync(int id, Todos todo);
        Task<bool> DeleteTodoAsync(int id);
        Task<Todos?> GetTodoWithTags(int todoId);
        Task AddTagToTodo(int todoId, int tagId);
        Task RemoveTagFromTodo(int todoId, int tagId);
        Task<List<Tags>> GetTagsForTodo(int todoId);
        Task<List<Todos>> GetTodosByUser(int userId);
        Task<List<Todos>> GetTodosByList(int listId);
    }
}