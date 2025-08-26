using MeowCore.Models;
using MeowCore.Models.RequestDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeowCore.Service.Interfaces
{
    public interface IListsService
    {
        Task<Lists?> GetListsById(int id);
        Task<List<Lists>?> GetListsAsync();
        Task<Lists> CreateListAsync(ListRequestDto list);
        Task<Lists?> UpdateListAsync(int id, ListRequestDto list);
        Task<bool> DeleteListAsync(int id);
    }
}