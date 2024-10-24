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
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public async Task<Movie> GetMovieAsync(int id)
        {
            return await context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await context.Movies.ToListAsync();
        }

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            if (movie == null)
                return false;

            try
            {
                var state = context.Entry(movie).State = movie.Id == default(int) ? EntityState.Added : EntityState.Modified;
                List<Category> categories = context.Categories.Where(c => movie.CategoryIds.Contains(c.Id)).ToList();
                if (state == EntityState.Modified)
                {
                    List<Category> removable = new List<Category>();
                    foreach (Category category in movie.Categories)
                    {
                        if (!categories.Contains(category))
                            removable.Add(category);
                        else
                            categories.Remove(category);
                    }

                    foreach (Category category in removable)
                    {
                        movie.Categories.Remove(category);
                    }
                }

                movie.Categories = movie.Categories != null ? categories.Concat(movie.Categories).ToList() : categories;

                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteMovieAsync(Movie movie)
        {
            if (movie == null)
                return false;

            context.Movies.Remove(movie);

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
