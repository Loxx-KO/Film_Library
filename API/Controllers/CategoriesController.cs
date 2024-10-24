using AutoMapper;
using DataAccessLayer.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models.InputModels.Categories;
using Entities.Models;
using Swashbuckle.Swagger.Annotations;
using API.Models.OutputModels.Categories;

namespace API.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get selected category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Category details</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category details", typeof(CategoryOutputModel))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty object")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);

            if (category == null)
                return NotFound();

            var outputCategory = _mapper.Map<CategoryOutputModel>(category);

            return Ok(outputCategory);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [SwaggerResponse(HttpStatusCode.OK, "List of categories", typeof(List<CategoryOutputModel>))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Empty list")]
        public async Task<IHttpActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();

            if (categories == null || !categories.Any())
                return NotFound();

            var outputCategories = new List<CategoryOutputModel>();

            foreach (Category category in categories)
            {
                outputCategories.Add(_mapper.Map<CategoryOutputModel>(category));
            }

            return Ok(outputCategories);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="model">Category details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category added")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Category addition failed")]
        public async Task<IHttpActionResult> Post(CategoryInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(model);

            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <param name="model">Category details</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Model is null or in invalid state")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Category update failed")]
        public async Task<IHttpActionResult> Put(int id, CategoryInputModel model)
        {
            if (model == null)
                return BadRequest("Model can't be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            _mapper.Map(model, category);

            var result = await _categoryRepository.SaveCategoryAsync(category);
            if (!result) return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete existing category
        /// </summary>
        /// <param name="id">Category identifier</param>
        /// <returns>Status</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Category deleted")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category doesn't exist")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Category deletion failed")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null) return NotFound();

            var result = await _categoryRepository.DeleteCategoryAsync(category);
            if (!result) return InternalServerError();

            return Ok();
        }
    }
}