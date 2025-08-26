using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
using MeowCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service
{
    public class ListsService : IListsService
    {
        public IListsRepository _repository { get; set; }

        public ListsService(IListsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Lists> CreateListAsync(ListRequestDto list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (string.IsNullOrWhiteSpace(list.Title))
                throw new ArgumentException("List title cannot be empty.");

            var listsEntity = new Lists
            {
                Title = list.Title,
            };

            return await _repository.CreateListAsync(listsEntity);
        }

        public async Task<bool> DeleteListAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid list id.");

            return await _repository.DeleteListAsync(id);
        }

        public async Task<List<Lists>?> GetListsAsync()
        {
            return await _repository.GetListsAsync();
        }

        public async Task<Lists?> GetListsById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid list id.");

            return await _repository.GetListsById(id);
        }

        public async Task<Lists?> UpdateListAsync(int id, ListRequestDto list)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid list id.");
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (string.IsNullOrWhiteSpace(list.Title))
                throw new ArgumentException("List title cannot be empty.");

            var listsEntity = new Lists
            {
                Id = id,
                Title = list.Title,
            };

            return await _repository.UpdateListAsync(id, listsEntity);
        }
    }
}
