using MeowCore.Data;
using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
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

        public async Task<Todos> CreateTodoAsync(TodoRequestDto todo, int userId)
        {
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            if (string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentException("Todo title cannot be empty.");

            var todoEntity = new Todos
            {
                Title = todo.Title,
                Description = todo.Description,
                Status = 0,
                ListId = todo.ListId,
            };

            return await _repository.CreateTodoAsync(todoEntity, userId);
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

        public async Task<Todos?> UpdateTodoAsync(int id, TodoRequestDto todo)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid todo id.");
            if (todo == null)
                throw new ArgumentNullException(nameof(todo));
            if (string.IsNullOrWhiteSpace(todo.Title))
                throw new ArgumentException("Todo title cannot be empty.");

            var todoEntity = new Todos
            {
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status
            };

            return await _repository.UpdateTodoAsync(id, todoEntity);
        }

        public async Task<List<Todos>> GetTodosByUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user id.");

            return await _repository.GetTodosByUser(userId);
        }

        public async Task<List<Todos>> GetTodosByList(int listId)
        {
            if (listId <= 0)
                throw new ArgumentException("Invalid list id.");

            return await _repository.GetTodosByList(listId);
        }
    }
}
