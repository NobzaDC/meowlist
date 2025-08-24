using Azure;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MeowCore.Data
{
    public class TodosRepository: ITodosRepository
    {
        private readonly MeowDbContext _context;

        public TodosRepository(MeowDbContext context)
        {
            _context = context;
        }

        public async Task<Todos?> GetTodosById(int id) => await _context.Todos.FindAsync(id);

        public async Task<List<Todos>?> GetTodosAsync() => await _context.Todos.ToListAsync();

        public async Task<Todos> CreateTodoAsync(Todos todo)
        {
            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<Todos?> UpdateTodoAsync(int id, Todos todo)
        {
            var dbTodo = await _context.Todos.FindAsync(id);

            if (dbTodo == null)
            {
                return null;
            }

            dbTodo.Title = todo.Title;
            dbTodo.Description = todo.Description;
            dbTodo.Status = todo.Status;

            await _context.SaveChangesAsync();
            return dbTodo;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null) return false;

            _context.Todos.Remove(todo);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Todos?> GetTodoWithTags(int todoId)
        {
            return await _context.Todos
                .Include(t => t.TodosTags)
                .ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t => t.Id == todoId);
        }

        // Asignar un Tag a un Todo
        public async Task AddTagToTodo(int todoId, int tagId)
        {
            var existing = await _context.Set<TodosTags>().FindAsync(todoId, tagId);
            if (existing == null)
            {
                _context.Add(new TodosTags { TodoId = todoId, TagId = tagId });
                await _context.SaveChangesAsync();
            }
        }

        // Remover un Tag de un Todo
        public async Task RemoveTagFromTodo(int todoId, int tagId)
        {
            var entity = await _context.Set<TodosTags>().FindAsync(todoId, tagId);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Obtener todos los Tags de un Todo
        public async Task<List<Tags>> GetTagsForTodo(int todoId)
        {
            return await _context.TodoTags
                .Where(tt => tt.TodoId == todoId)
                .Select(tt => tt.Tag)
                .ToListAsync();
        }
    }
}
