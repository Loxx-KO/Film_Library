using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class DirectorRepository : BaseRepository, IDirectorRepository
    {
        public async Task<Director> GetDirectorAsync(int id)
        {
            return await context.Directors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Director>> GetDirectorsAsync()
        {
            return await context.Directors.ToListAsync();
        }

        public async Task<bool> SaveDirectorAsync(Director director)
        {
            if (director == null)
                return false;

            try
            {
                context.Entry(director).State = director.Id == default(int) ? EntityState.Added : EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteDirectorAsync(Director director)
        {
            if (director == null)
                return false;

            context.Directors.Remove(director);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
