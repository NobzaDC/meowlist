using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service.Interfaces
{
    public interface ITodosService
    {
        Task<Todos?> GetTodosById(int id);
        Task<List<Todos>?> GetTodosAsync();
        Task<Todos> CreateTodoAsync(Todos todo);
        Task<Todos?> UpdateTodoAsync(int id, Todos todo);
        Task<bool> DeleteTodoAsync(int id);
        Task<Todos?> GetTodoWithTags(int todoId);
        Task AddTagToTodo(int todoId, int tagId);
        Task RemoveTagFromTodo(int todoId, int tagId);
        Task<List<Tags>> GetTagsForTodo(int todoId);
    }
}