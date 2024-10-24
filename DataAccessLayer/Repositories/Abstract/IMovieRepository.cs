using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieAsync(int id);
        Task<List<Movie>> GetMoviesAsync();
        Task<bool> SaveMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(Movie movie);
    }
}
