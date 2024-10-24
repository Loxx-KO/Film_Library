
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.InputModels.Movies;
using API.Models.OutputModels.Movies;
using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using Swashbuckle.Swagger.Annotations;

namespace API.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected movie
        /// </summary>
        /// <param name="id">Movie identifier</param>
        /// <returns>Movie details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Movie details", typeof(MovieOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);

            if (movie == null)
                return NotFound();

            var outputMovie = _mapper.Map<MovieOutputModel>(movie);

            return Ok(outputMovie);
        }

        /// <summary>
        /// Get all movies
        /// </summary>
        /// <returns>List of movies</returns>
        [SwaggerResponse(HttpStatusCode.OK, "List of movies", typeof(List<MovieOutputModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var movies = await _movieRepository.GetMoviesAsync();

            if (movies == null || !movies.Any())
                return NotFound();

            var outputMovies = new List<MovieOutputModel>();

            foreach (Movie movie in movies)
            {
                outputMovies.Add(_mapper.Map<MovieOutputModel>(movie));
            }

            return Ok(outputMovies);
        }

        /// <summary>
        /// Create new movie
        /// </summary>
        /// <param name="model">Movie details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Movie added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Movie addition failed")]
        public async Task<IHttpActionResult> Post(MovieInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = _mapper.Map<Movie>(model);

            var result = await _movieRepository.SaveMovieAsync(movie);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Update existing movie
        /// </summary>
        /// <param name="id">Movie identifier</param>
        /// <param name="model">Movie details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Movie updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Movie doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Movie update failed")]
        public async Task<IHttpActionResult> Put(int id, MovieInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = await _movieRepository.GetMovieAsync(id);
            if (movie == null)
                return NotFound();

            _mapper.Map(model, movie);

            var result = await _movieRepository.SaveMovieAsync(movie);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete existing movie
        /// </summary>
        /// <param name="id">Movie identifier</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Movie deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Movie doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Movie deletion failed")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var movie = await _movieRepository.GetMovieAsync(id);
            if (movie == null) return NotFound();

            var result = await _movieRepository.DeleteMovieAsync(movie);
            if (!result) return InternalServerError();

            return Ok();
        }
    }
}