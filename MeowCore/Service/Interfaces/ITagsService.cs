using MeowCore.Models;
using MeowCore.Models.RequestDTOs;

namespace MeowCore.Service.Interfaces
{
    public interface ITagsService
    {
        Task<Tags?> GetTagsById(int id);
        Task<List<Tags>?> GetTagsAsync();
        Task<Tags> CreateTagAsync(TagRequestDto tag);
        Task<Tags?> UpdateTagAsync(int id, TagRequestDto tag);
        Task<bool> DeleteTagAsync(int id);
    }
}