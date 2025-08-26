using MeowCore.Data.Interfaces;
using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
using MeowCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service
{
    public class TagsService : ITagsService
    {
        public ITagsRepository _repository { get; set; }

        public TagsService(ITagsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tags> CreateTagAsync(TagRequestDto tag)
        {
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            if (string.IsNullOrWhiteSpace(tag.Name))
                throw new ArgumentException("Tag name cannot be empty.");

            var tagsEntity = new Tags
            {
                Name = tag.Name,
                Color = tag.Color
            };

            return await _repository.CreateTagAsync(tagsEntity);
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid tag id.");

            return await _repository.DeleteTagAsync(id);
        }

        public async Task<List<Tags>?> GetTagsAsync()
        {
            return await _repository.GetTagsAsync();
        }

        public async Task<Tags?> GetTagsById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid tag id.");

            return await _repository.GetTagsById(id);
        }

        public async Task<Tags?> UpdateTagAsync(int id, TagRequestDto tag)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid tag id.");
            if (tag == null)
                throw new ArgumentNullException(nameof(tag));
            if (string.IsNullOrWhiteSpace(tag.Name))
                throw new ArgumentException("Tag name cannot be empty.");

            var tagsEntity = new Tags
            {
                Name = tag.Name,
                Color = tag.Color
            };

            return await _repository.UpdateTagAsync(id, tagsEntity);
        }
    }
}
