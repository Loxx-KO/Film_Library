using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface IDirectorRepository
    {
        Task<Director> GetDirectorAsync(int id);
        Task<List<Director>> GetDirectorsAsync();
        Task<bool> SaveDirectorAsync(Director director);
        Task<bool> DeleteDirectorAsync(Director director);
    }
}
