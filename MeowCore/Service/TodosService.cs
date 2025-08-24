using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service
{
    public class TodosService : ITodosService
    {
        public ITodosRepository _repository { get; set; }

        public TodosService(ITodosRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTagToTodo(int todoId, int tagId)
        {
            if (todoId <= 0)
                throw new ArgumentException("Invalid todo id.");
            if (tagId <= 0)
                throw new ArgumentException("Invalid tag id.");

            await _repository.AddTagToTodo(todoId, tagId);
        }

        public async Task<Todos> CreateTodoAsync(Todos todo)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            if (string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentException("Todo title cannot be empty.");

            return await _repository.CreateTodoAsync(todo);
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid todo id.");

            return await _repository.DeleteTodoAsync(id);
        }

        public async Task<List<Tags>> GetTagsForTodo(int todoId)
        {
            if (todoId <= 0)
                throw new ArgumentException("Invalid todo id.");

            return await _repository.GetTagsForTodo(todoId);
        }

        public async Task<List<Todos>?> GetTodosAsync()
        {
            return await _repository.GetTodosAsync();
        }

        public async Task<Todos?> GetTodosById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid todo id.");

            return await _repository.GetTodosById(id);
        }

        public async Task<Todos?> GetTodoWithTags(int todoId)
        {
            if (todoId <= 0)
                throw new ArgumentException("Invalid todo id.");

            return await _repository.GetTodoWithTags(todoId);
        }

        public async Task RemoveTagFromTodo(int todoId, int tagId)
        {
            if (todoId <= 0)
                throw new ArgumentException("Invalid todo id.");
            if (tagId <= 0)
                throw new ArgumentException("Invalid tag id.");

            await _repository.RemoveTagFromTodo(todoId, tagId);
        }

        public async Task<Todos?> UpdateTodoAsync(int id, Todos todo)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid todo id.");
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            if (string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentException("Todo title cannot be empty.");

            return await _repository.UpdateTodoAsync(id, todo);
        }
    }
}
