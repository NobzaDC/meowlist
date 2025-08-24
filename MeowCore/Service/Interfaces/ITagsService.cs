using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service.Interfaces
{
    public interface ITagsService
    {
        Task<Tags?> GetTagsById(int id);
        Task<List<Tags>?> GetTagsAsync();
        Task<Tags> CreateTagAsync(Tags tag);
        Task<Tags?> UpdateTagAsync(int id, Tags tag);
        Task<bool> DeleteTagAsync(int id);
    }
}