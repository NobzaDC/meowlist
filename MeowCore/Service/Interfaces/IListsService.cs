using MeowCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service.Interfaces
{
    public interface IListsService
    {
        Task<Lists?> GetListsById(int id);
        Task<List<Lists>?> GetListsAsync();
        Task<Lists> CreateListAsync(Lists list);
        Task<Lists?> UpdateListAsync(int id, Lists list);
        Task<bool> DeleteListAsync(int id);
    }
}