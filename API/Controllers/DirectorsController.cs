using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.InputModels.Directors;
using API.Models.OutputModels.Directors;
using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using Swashbuckle.Swagger.Annotations;

namespace API.Controllers
{
    public class DirectorsController : ApiController
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorsController(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected director
        /// </summary>
        /// <param name="id">Director identifier</param>
        /// <returns>Director details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Director details", typeof(DirectorOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var director = await _directorRepository.GetDirectorAsync(id);

            if (director == null)
                return NotFound();

            var outputDirector = _mapper.Map<DirectorOutputModel>(director);

            return Ok(outputDirector);
        }

        /// <summary>
        /// Get all directors
        /// </summary>
        /// <returns>List of directors</returns>
        [SwaggerResponse(HttpStatusCode.OK, "List of directors", typeof(List<DirectorOutputModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var directors = await _directorRepository.GetDirectorsAsync();

            if (directors == null || !directors.Any())
                return NotFound();

            var outputDirectors = new List<DirectorOutputModel>();

            foreach (Director director in directors)
            {
                outputDirectors.Add(_mapper.Map<DirectorOutputModel>(director));
            }

            return Ok(outputDirectors);
        }

        /// <summary>
        /// Create new director
        /// </summary>
        /// <param name="model">Director details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Director added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Director addition failed")]
        public async Task<IHttpActionResult> Post(DirectorInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var director = _mapper.Map<Director>(model);

            var result = await _directorRepository.SaveDirectorAsync(director);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Update existing director
        /// </summary>
        /// <param name="id">Director identifier</param>
        /// <param name="model">Director details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Director updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Director doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Director update failed")]
        public async Task<IHttpActionResult> Put(int id, DirectorInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var director = await _directorRepository.GetDirectorAsync(id);
            if (director == null)
                return NotFound();

            _mapper.Map(model, director);

            var result = await _directorRepository.SaveDirectorAsync(director);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete existing director
        /// </summary>
        /// <param name="id">Director identifier</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Director deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Director doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Director deletion failed")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var director = await _directorRepository.GetDirectorAsync(id);
            if (director == null) return NotFound();

            var result = await _directorRepository.DeleteDirectorAsync(director);
            if (!result) return InternalServerError();

            return Ok();
        }
    }
}