using MeowCore.Data.Interfaces;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MeowCore.Data
{
    public class ListsRepository: IListsRepository
    {
        private readonly MeowDbContext _context;

        public ListsRepository(MeowDbContext context)
        {
            _context = context;
        }

        public async Task<Lists?> GetListsById(int id) => await _context.Lists.FindAsync(id);

        public async Task<List<Lists>?> GetListsAsync() => await _context.Lists.ToListAsync();

        public async Task<Lists> CreateListAsync(Lists list)
        {
            await _context.AddAsync(list);
            await _context.SaveChangesAsync();
            return list;
        }

        public async Task<Lists?> UpdateListAsync(int id, Lists list)
        {
            var dbList = await _context.Lists.FindAsync(id);

            if (dbList == null)
            {
                return null;
            }

            dbList.Title = list.Title;

            await _context.SaveChangesAsync();
            return dbList;
        }

        public async Task<bool> DeleteListAsync(int id)
        {
            var list = await _context.Lists.FindAsync(id);

            if (list == null) return false;

            _context.Lists.Remove(list);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
