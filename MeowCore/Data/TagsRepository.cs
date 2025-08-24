using MeowCore.Data.Interfaces;
using MeowCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MeowCore.Data
{
    public class TagsRepository: ITagsRepository 
    {
        private readonly MeowDbContext _context;

        public TagsRepository(MeowDbContext context)
        {
            _context = context;
        }

        public async Task<Tags?> GetTagsById(int id) => await _context.Tags.FindAsync(id);

        public async Task<List<Tags>?> GetTagsAsync() => await _context.Tags.ToListAsync();

        public async Task<Tags> CreateTagAsync(Tags tag)
        {
            await _context.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tags?> UpdateTagAsync(int id, Tags tag)
        {
            var dbTag = await _context.Tags.FindAsync(id);

            if (dbTag == null)
            {
                return null;
            }

            dbTag.Name = tag.Name;
            dbTag.Color = tag.Name;

            await _context.SaveChangesAsync();
            return dbTag;
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null) return false;

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
